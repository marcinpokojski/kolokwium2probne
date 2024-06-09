using Microsoft.EntityFrameworkCore;
using probnygakko.Context;
using probnygakko.DTOs;
using probnygakko.Models;

namespace probnygakko.Repositories;

public class MuzykRepository : IMuzykRepository
{
    private readonly SQLContext _sqlContext;

    public MuzykRepository(SQLContext sqlContext)
    {
        _sqlContext = sqlContext;
    }

    // public async Task<bool> DoesMuzykExist(string FirstName, string LastName)
    // {
    //     var result = await _sqlContext.Muzycy.AnyAsync(x => x.FirstName == FirstName && x.LastName == LastName);
    //     return result;
    // }
    //
    // public Task<bool> DoesMuzykExist(int id)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<int> AddMuzyk(MuzykDTOToAdd muzykDtoToAdd)
    {
        await using var transaction = await _sqlContext.Database.BeginTransactionAsync();
        try
        {
            //dodawanie muzyka
            Muzyk muzyk = new Muzyk()
            {
                FirstName = muzykDtoToAdd.FirstName,
                LastName = muzykDtoToAdd.LastName,
                Pseudonim = muzykDtoToAdd.Pseudonim
            };
            await _sqlContext.Muzycy.AddAsync(muzyk);
            await _sqlContext.SaveChangesAsync();
            //sprawdzanie czy utwor istnieje w bazie, jak nie to go dodajemy
             foreach (var utwor in muzykDtoToAdd.Utwory)
             {
                 var ifExist = await _sqlContext.Utwory.AnyAsync(x => x.NazwaUtworu == utwor.NazwaUtworu);
                 if (!ifExist)
                 {
                     Utwor utworToAdd = new Utwor
                     {
                         NazwaUtworu = utwor.NazwaUtworu,
                         CzasTrwania = utwor.CzasTrwania,
                         IdAlbum = utwor.IdAlbum
                     };
                     await _sqlContext.Utwory.AddAsync(utworToAdd);
                     
                 }
            
                 await _sqlContext.SaveChangesAsync();
             }
            
            //łączenie muzyka z utworem do tabeli Wykonawca utworu
             foreach (var item in muzykDtoToAdd.Utwory)
             {
                 WykonawcaUtworu wykonawcaUtworu = new WykonawcaUtworu
                 {
                     IdMuzyk = muzyk.IdMuzyk,
                     IdUtwor = await _sqlContext.Utwory.Where(x => x.NazwaUtworu == item.NazwaUtworu)
                         .Select(x => x.IdUtwor).FirstOrDefaultAsync()
                 };
                 await _sqlContext.Wykonawcy.AddAsync(wykonawcaUtworu);
             }
            
             await _sqlContext.SaveChangesAsync();
             await transaction.CommitAsync(); //PAMIETAC O COMMICIE

        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return 0;
        }

        return 1;
    }
    

    public async Task<MuzykDTO> GetMuzykInfo(int id)
    {
        var muzyk = await _sqlContext.Muzycy
            .Include(m => m.Wykonawcy)
            .ThenInclude(w => w.Utwory)
            .FirstOrDefaultAsync(x => x.IdMuzyk == id);

        if (muzyk == null)
        {
            return null;
        }

        // var utwory =  await _sqlContext.Wykonawcy.Where(w => w.IdMuzyk == id)
        //     .Select(w => w.Utwory)
        //     .Distinct().ToListAsync();
        
        var utwory = await _sqlContext.Utwory
            .Where(u => u.Wykonawcy.Any(w => w.IdMuzyk == id))
            .ToListAsync();

        var result = new MuzykDTO
        {
            IdMuzyk = muzyk.IdMuzyk,
            FirstName = muzyk.FirstName,
            LastName = muzyk.LastName,
            Pseudonim = muzyk.Pseudonim,
            Utwory = utwory.Select(u => new UtworDTO
            {
                IdUtwor = u.IdUtwor,
                NazwaUtworu = u.NazwaUtworu,
                CzasTrwania = u.CzasTrwania
            }).ToList()

        };
        
        return result;
    }
}

    


    