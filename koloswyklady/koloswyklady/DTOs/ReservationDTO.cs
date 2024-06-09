using System.ComponentModel.DataAnnotations;
using koloswyklady.Models;

namespace koloswyklady.DTOs;

public class ReservationDTO
{
    public int IdClient { get; set; }
     public string Name { get; set; }
     public string LastName { get; set; }
     public DateTime Birthday { get; set; }
      public string Pesel { get; set; }
      public string Emial { get; set; }
    public string ClientCategory { get; set; }
    public List<ReservationDetailDTO> Reservations { get; set; }
}

public class ReservationDetailDTO
{
     public int IdReservation { get; set; }
     public int IdClient { get; set; }
     public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
     public int IdBoatStandard { get; set; }
     public int Capacity { get; set; }
    public int NumberOfBoats { get; set; }
    public int Fullfilled { get; set; }
        public double?  Price { get; set; }
        [MaxLength(100)] public string? CancelReason { get; set; }
}