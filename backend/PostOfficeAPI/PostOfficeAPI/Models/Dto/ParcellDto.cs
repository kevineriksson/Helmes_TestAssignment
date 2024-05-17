using PostOfficeAPI.Contracts;
using PostOfficeAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ParcelDto : IEntity
{
    public string Id { get; set; }
    public string RecipientName { get; set; }
    public string DestinationCountry { get; set; }
    public decimal Weight { get; set; }
    public decimal Price { get; set; }
    public string? BagId { get; set; }
}
