using koloswyklady.Context;
using koloswyklady.DTOs;
using koloswyklady.Models;
using Microsoft.EntityFrameworkCore;

namespace koloswyklady.Repositories;

public class SwimRepository : ISwimRepository
{
    private readonly Sqlcontext _sqlcontext;

    public SwimRepository(Sqlcontext sqlcontext)
    {
        _sqlcontext = sqlcontext;
    }

    public async Task<ReservationDTO> GetClientDetails(int id)
    {
        var client = await _sqlcontext.Clients
            .Include(c => c.Reservations)
            .ThenInclude(c=> c.BoatStandard)
            .Include(c=>c.ClientCateogry)
            .FirstOrDefaultAsync(x => x.IdClient == id);
        if (client == null)
        {
            return null;
        }

        var reservations = await _sqlcontext.Reservations
            .Include(r => r.Client)
            .Select(r => new ReservationDetailDTO
            {
                IdReservation = r.IdReservation,
                IdClient = r.IdClient,
                DateFrom = r.DateFrom,
                DateTo = r.DateTo,
                Capacity = r.Capacity,
                CancelReason = r.CancelReason,
                NumberOfBoats = r.NumberOfBoats,
                IdBoatStandard = r.IdBoatStandard
            }).ToListAsync();
        var result = new ReservationDTO
        {
            IdClient = client.IdClient,
            Name = client.Name,
            LastName = client.LastName,
            Birthday = client.Birthday,
            Pesel = client.Pesel,
            Emial = client.Emial,
            ClientCategory = client.ClientCateogry.Name,
            Reservations = reservations
        };
        return result;
    }
}