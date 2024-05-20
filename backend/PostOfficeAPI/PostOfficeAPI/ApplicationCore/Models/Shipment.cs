using PostOfficeAPI.ApplicationCore.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOfficeAPI.ApplicationCore.Models
{
    public class Shipment : IEntity
    {
        [Key]
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{3}-[A-Za-z0-9]{6}$", ErrorMessage = "Shipment number must be in the format 'XXX-XXXXXX'")]
        public string Id { get; set; }

        [Required]
        public string AirportCode { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]{2}\d{4}$", ErrorMessage = "Flight number must be in the format 'LLNNNN'")]
        public string FlightNumber { get; set; }

        [Required]
        public DateTime FlightDate { get; set; }
        public List<Bag> Bags { get; set; }
        public bool isFinalized { get; set; }
    }
}
