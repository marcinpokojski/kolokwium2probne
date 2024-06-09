using Microsoft.EntityFrameworkCore;
using zad10v2.Context;
using zad10v2.DTOs;
using zad10v2.Enums;
using zad10v2.Models;

namespace zad10v2.Repositories;

public class AptekaRepository : IAptekaRepsitory
{
    private readonly SqlContext _sqlContext;

    public AptekaRepository(SqlContext sqlContext)
    {
        _sqlContext = sqlContext;
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
}