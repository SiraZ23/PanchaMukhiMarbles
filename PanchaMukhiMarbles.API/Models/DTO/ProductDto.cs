namespace PanchaMukhiMarbles.API.Models.DTO
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public float Price { get; set; }
        public string? MadeIn { get; set; }
       
        public string? Thickness { get; set; }
        public string? CompanyName { get; set; }
        public string InStock { get; set; }

        public CategoryDto Category { get; set; }
    }
}
