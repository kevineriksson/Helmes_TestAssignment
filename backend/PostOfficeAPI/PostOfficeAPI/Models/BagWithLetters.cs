using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PostOfficeAPI.Models
{
    public class BagWithLetters : Bag
    {

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Count of letters must be at least 1")]
        public int CountOfLetters { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Weight { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
