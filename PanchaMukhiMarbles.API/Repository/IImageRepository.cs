using PanchaMukhiMarbles.API.Models.Domain;

namespace PanchaMukhiMarbles.API.Repository
{
    public interface IImageRepository
    {
        Task<Image>Upload(Image image);
    }
}
