using PostOfficeAPI.ApplicationCore.Contracts.Entities;
using PostOfficeAPI.ApplicationCore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Parcel : IEntity
{
    [Key]
    [Required]
    [RegularExpression(@"^[A-Za-z]{2}\d{6}[A-Za-z]{2}$", ErrorMessage = "Parcel number must be in the format 'LLNNNNNNLL'")]
    public string Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Recipient name must not exceed 100 characters")]
    public string RecipientName { get; set; }

    [Required]
    [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Destination country must be a 2-letter code")]
    public string DestinationCountry { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 3)")]
    public decimal Weight { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public string? BagId { get; set; }
    public BagWithParcels? BagWithParcels { get; set; }
}
