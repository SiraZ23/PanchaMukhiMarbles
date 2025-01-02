using AutoMapper;
using PanchaMukhiMarbles.API.Models.Domain;
using PanchaMukhiMarbles.API.Models.DTO;

namespace PanchaMukhiMarbles.API.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AddProductRequestDto,Product>().ReverseMap();
            CreateMap<ProductDto,Product>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<UpdateProductRequestDto,Product>().ReverseMap();
        }
    }
}
