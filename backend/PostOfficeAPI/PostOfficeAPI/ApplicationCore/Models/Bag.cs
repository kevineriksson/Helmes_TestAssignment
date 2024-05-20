using PostOfficeAPI.ApplicationCore.Contracts.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOfficeAPI.ApplicationCore.Models
{
    public class Bag : IEntity
    {
        [StringLength(15, ErrorMessage = "Bag number must not exceed 15 characters")]
        public string Id { get; set; }
        public string? ShipmentId { get; set; }
        public Shipment? Shipment { get; set; }
        public string BagType { get; set; }
        //public bool IsFinalized { get; set; }
    }
}
