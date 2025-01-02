namespace PanchaMukhiMarbles.API.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public float Price { get; set; }
        public string? MadeIn { get; set; }
        public Guid CategoryId { get; set; }
        public string? Thickness { get; set; }
        public string? CompanyName { get; set; }
        public string? InStock { get; set; }

        
        //Navigation Property
        public Category Category { get; set; }
    }
}
