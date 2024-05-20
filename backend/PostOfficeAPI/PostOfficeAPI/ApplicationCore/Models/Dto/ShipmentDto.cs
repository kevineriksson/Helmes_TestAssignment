using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PostOfficeAPI.ApplicationCore.Models.Dto
{
    public class ShipmentDto
    {
        public string Id { get; set; }
        public string AirportCode { get; set; }
        public string FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
        public bool IsFinalized { get; set; }
    }
}
