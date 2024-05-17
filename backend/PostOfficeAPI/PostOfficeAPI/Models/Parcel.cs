using PostOfficeAPI.Contracts;
using PostOfficeAPI.Models; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Parcel : IEntity
{
    // Ensuring the parcel number has the specified format and is unique in the database
    [Key]
    [Required]
    [RegularExpression(@"^[A-Za-z]{2}\d{6}[A-Za-z]{2}$", ErrorMessage = "Parcel number must be in the format 'LLNNNNNNLL'")]
    public string Id { get; set; }

    // Recipient name must not exceed 100 characters
    [Required]
    [StringLength(100, ErrorMessage = "Recipient name must not exceed 100 characters")]
    public string RecipientName { get; set; }

    // Destination country must be a 2-letter code
    [Required]
    [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Destination country must be a 2-letter code")]
    public string DestinationCountry { get; set; }

    // Numeric field for weight, assuming SQL Server can handle the precision if properly configured
    [Required]
    [Column(TypeName = "decimal(18, 3)")]
    public decimal Weight { get; set; }

    // Price with a precision of 2 decimal places
    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public string? BagId { get; set; }
    public BagWithParcels? BagWithParcels { get; set; }
}
