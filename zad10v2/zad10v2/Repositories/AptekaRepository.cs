using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using zad10v2.Context;
using zad10v2.DTOs;
using zad10v2.Enums;
using zad10v2.Models;

namespace zad10v2.Repositories;

public class AptekaRepository : IAptekaRepsitory
{
    private readonly SqlContext _sqlContext;
    private readonly IConfiguration _configuration;

    public AptekaRepository(IConfiguration configuration, SqlContext sqlContext)
    {
        _sqlContext = sqlContext;
        _configuration = configuration;
    }

    public async Task<Errors> AddNewRecepta(PrescriptionDTO prescriptionDto)
    {
        await using var transaction = await _sqlContext.Database.BeginTransactionAsync();
        try
        {
            
            //dodawanie pacjenta
           
            var patient= await _sqlContext.Patients
                .Where(x => x.FirstName == prescriptionDto.Patient.FirstName &&
                            x.LastName == prescriptionDto.Patient.LastName)
                .FirstOrDefaultAsync();
            if (patient == null)
            {
                 patient = new Patient
                {
                    FirstName = prescriptionDto.Patient.FirstName,
                    LastName = prescriptionDto.Patient.LastName,
                    Birthdate = prescriptionDto.Patient.Birthdate
                };
                await _sqlContext.Patients.AddAsync(patient);
                await _sqlContext.SaveChangesAsync();
            }

            var length = prescriptionDto.Medicaments.Count;

            if (length > 10)
            {
                return Errors.TooManyMedicaments;
            }

            foreach (var medicamentE in prescriptionDto.Medicaments)
            {
                var ifMedicamentExist = await _sqlContext.Medicaments.AnyAsync(x => x.Name == medicamentE.Name);
                if (!ifMedicamentExist)
                {
                    return Errors.MedicamentDoesntExist;
                }
            }
            
            if (prescriptionDto.DueDate < prescriptionDto.Date)
            {
                return Errors.WrongDate;
            }

            Prescription prescription = new Prescription
            {
                Date = prescriptionDto.Date,
                DueDate = prescriptionDto.DueDate,
                IdPatient = patient.IdPatient,
                IdDoctor = 1
            };
            await _sqlContext.Prescriptions.AddAsync(prescription);
            await _sqlContext.SaveChangesAsync();
            
            foreach (var medicamentE in prescriptionDto.Medicaments)
            {
                Prescription_Medicament prescriptionMedicament = new Prescription_Medicament
                {
                    IdMedicament = await _sqlContext.Medicaments.Where(x => x.Name == medicamentE.Name)
                        .Select(x => x.IdMedicament).FirstAsync(),
                    IdPrescription = prescription.IdPrescription,
                    Details = prescriptionDto.Details
                };
                await _sqlContext.PrescriptionMedicaments.AddAsync(prescriptionMedicament);
            }

            await _sqlContext.SaveChangesAsync();
            await transaction.CommitAsync();




        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return Errors.ErrorDb;
        }

        return Errors.Ok;

    }

    public async Task<bool> DoesPatientExixt(PrescriptionDTO prescriptionDto)
    {
        var patient = await _sqlContext.Patients
            .Where(x => x.FirstName == prescriptionDto.Patient.FirstName &&
                        x.LastName == prescriptionDto.Patient.LastName)
            .FirstOrDefaultAsync();

        if (patient == null)
        {
            return false;
        }

        return true;
    }

    public async Task<PatientDTOToShow> GetPatientInfo(int id)
    {
        var ifPatientExist = await _sqlContext.Patients.AnyAsync(x => x.IdPatient == id);
        if (!ifPatientExist)
        {
            return null;
        }

        var patient = await _sqlContext.Patients.Include(x => x.Prescriptions)
            .ThenInclude(x => x.PrescriptionMedicaments)
            .ThenInclude(x => x.Medicament)
            .Include(x => x.Prescriptions)
            .ThenInclude(x => x.Doctor).Where(x => x.IdPatient == id).FirstOrDefaultAsync();


        PatientDTOToShow patientDtoToShow = new PatientDTOToShow
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            Prescriptions = patient.Prescriptions.OrderBy(x => x.DueDate).Select(p => new PrescriptionDTOToshow
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,

                Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentDtoToShow
                {
                    IdMedicament = pm.IdMedicament,
                    Description = pm.Details
                }).ToList(),
                Doctor = new DoctorDTOToShow
                {
                    IdDoctor = p.Doctor.IdDoctor,
                    FirstName = p.Doctor.FirstName
                }
            }).ToList()


        };
        return patientDtoToShow;
    }
    
    public async Task<Errors> AddUser(RegisterRequest model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);
        
        var user = new AppUser()
        {
            Email = model.Email,
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };
        
        await _sqlContext.Users.AddAsync(user);
        await _sqlContext.SaveChangesAsync();
    
        return Errors.Ok;
    }
    
    public async Task<TokenResult> LoginUser(LoginRequest loginRequest)
    {
        AppUser user = await _sqlContext.Users.Where(u => u.Login == loginRequest.Login).FirstOrDefaultAsync();
    
        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);
    
        if (passwordHashFromDb != curHashedPassword)
        {
            return null;
        }
    
    
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "marcin"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
        };
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
    
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );
    
        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _sqlContext.SaveChangesAsync();
        
        return new TokenResult{
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken =  user.RefreshToken
            };
    }
    
    public async Task<TokenResult> RefreshUser(RefreshTokenRequest refreshToken)
    {
        AppUser user = await _sqlContext.Users.Where(u => u.RefreshToken == refreshToken.RefreshToken).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }
    
        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
        
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "marcin"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
        };
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
    
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );
    
        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _sqlContext.SaveChangesAsync();
    
        return new TokenResult{
            accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            refreshToken = user.RefreshToken
        };
    }
}