using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wa10.DTOs;
namespace wa10.Repositories;



public class TravelRepository : ITravelRepository
{
    private readonly SqlContext _sqlContext;

    public TravelRepository(SqlContext sqlContext)
    {
        _sqlContext = sqlContext;
    }

    public async Task<EventListDTO> GetListOfEvents(bool withDateend)
    {
        if (withDateend)
        {
            var result = await _sqlContext.Events.Include(x => x.EventOrganisers).ThenInclude(x => x.Organiser).Select(p =>
                new EventDTOtoshow()
                {
                    IdEvent = p.IdEvent,
                    Name = p.Name,
                    DateFrom = p.DateFrom,
                    DateTo = p.DateTo,
                    MainOrganisers = p.EventOrganisers.Where(x => x.MainOrganiser == true).Select(eo =>
                        new OrganiserDTO()
                        {
                            IdOrganiser = eo.IdOrganiser,
                            Name = eo.Organiser.Name
                        }).ToList(),
                    SubOrganisers = p.EventOrganisers.Where(x => x.MainOrganiser == false).Select(eo =>
                        new OrganiserDTO()
                        {
                            IdOrganiser = eo.IdOrganiser,
                            Name = eo.Organiser.Name
                        }).ToList(),
                }).ToListAsync();

            var lista = new EventListDTO()
            {
                EventsList = result
            };
            return lista;
        }
        else
        {
            var result = await _sqlContext.Events.Include(x => x.EventOrganisers).ThenInclude(x => x.Organiser)
                .Where(x => x.DateTo == null).Select(p =>
                    new EventDTOtoshow()
                    {
                        IdEvent = p.IdEvent,
                        Name = p.Name,
                        DateFrom = p.DateFrom,
                        DateTo = p.DateTo,
                        MainOrganisers = p.EventOrganisers.Where(x => x.MainOrganiser == true).Select(eo =>
                            new OrganiserDTO()
                            {
                                IdOrganiser = eo.IdOrganiser,
                                Name = eo.Organiser.Name
                            }).ToList(),
                        SubOrganisers = p.EventOrganisers.Where(x => x.MainOrganiser == false).Select(eo =>
                            new OrganiserDTO()
                            {
                                IdOrganiser = eo.IdOrganiser,
                                Name = eo.Organiser.Name
                            }).ToList(),
                    }).ToListAsync();
            var lista = new EventListDTO()
            {
                EventsList = result
            };
            return lista;
        }
        
    }
    
    public async Task<ResultDTO> DeleteEvent(int id)
    {
        //dodac czy event istnieje
        var eventExist = await _sqlContext.Events.AnyAsync(e => e.IdEvent == id);
        if (!eventExist)
        {
            return new ResultDTO(404, "nie ma takiego eventu");
        }
        var event1 = await _sqlContext.Events
            .Include(x => x.EventOrganisers)
            .Where(x => x.IdEvent == id)
            .FirstOrDefaultAsync();
        if (event1 == null)
        {
            return new ResultDTO(404, "nie ma takiego eventu");
        }

        if (event1.DateFrom < DateTime.Now || event1.DateTo != null)
        {
            var mainOrganisorCounter = event1.EventOrganisers.Count(x => x.MainOrganiser);
            if (mainOrganisorCounter < 3)
            {
                _sqlContext.Events.Remove(event1);
                await _sqlContext.SaveChangesAsync();
                return new ResultDTO(200, "ok");
            }
            else
            {
                return new ResultDTO(404, "ponad 3 main organisers");
            }
        }
        else
        {
            return new ResultDTO(404, "blad");
        }
    }

}

