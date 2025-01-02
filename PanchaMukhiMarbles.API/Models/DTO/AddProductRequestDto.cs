using System.ComponentModel.DataAnnotations;

namespace PanchaMukhiMarbles.API.Models.DTO
{
    public class AddProductRequestDto
    {
        [Required]
        [MinLength(4,  ErrorMessage="Name Has To Be More than 4 Characters")]
        [MaxLength(15, ErrorMessage ="Name Cannot Be More Than 15 Characters")]
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        [Required]

        public float Price { get; set; }
        public string? MadeIn { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
        public string? Thickness { get; set; }
        public string? CompanyName { get; set; }

        [Required]
        public string InStock { get; set; }
    }
}
