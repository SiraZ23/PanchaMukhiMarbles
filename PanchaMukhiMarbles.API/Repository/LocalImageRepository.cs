using PanchaMukhiMarbles.API.Data;
using PanchaMukhiMarbles.API.Models.Domain;

namespace PanchaMukhiMarbles.API.Repository
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly PanchaMukhiMarblesDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,
            PanchaMukhiMarblesDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
           var LocalFilePath = Path.Combine(webHostEnvironment.ContentRootPath,"Images",$"{image.FileName}{image.FileExtension}");
            
            //Upload Image To Local Folder
            using var stream = new FileStream(LocalFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //https://Localhost:1234/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            //Add Images To Image Table
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
