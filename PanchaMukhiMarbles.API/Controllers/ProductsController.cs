using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PanchaMukhiMarbles.API.CustomActionFilters;
using PanchaMukhiMarbles.API.Data;
using PanchaMukhiMarbles.API.Models.Domain;
using PanchaMukhiMarbles.API.Models.DTO;
using PanchaMukhiMarbles.API.Repository;

namespace PanchaMukhiMarbles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PanchaMukhiMarblesDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public ProductsController(IMapper mapper, IProductRepository productRepository)
        {

            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] AddProductRequestDto addProductRequestDto)
        {
            //Map DTO(addproductrequestDto) To Domain Model
            var productDomainModel = mapper.Map<Product>(addProductRequestDto);

            await productRepository.CreateAsync(productDomainModel);

            //Map Domain Model To DTO
            return Ok(mapper.Map<ProductDto>(productDomainModel));
        }

        [HttpGet]
        [ValidateModel]
        public async Task<IActionResult> GetAllAsync()
        {
            var productDomainModel = await productRepository.GetAllAsync();

            //Map Domain Model To DTO
            return Ok(mapper.Map<List<ProductDto>>(productDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> GetByIdAsync([FromRoute]Guid id)
        {
            var productDomainModel=await productRepository.GetByIdAsync(id);
            if (productDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model To DTO
            return Ok(mapper.Map<ProductDto>(productDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateProductRequestDto updateProductRequestDto)
        {
            //Map DTO To Domain Model
            var productDomainModel=mapper.Map<Product>(updateProductRequestDto);
            productDomainModel=await productRepository.UpdateAsync(id, productDomainModel);    
            if (productDomainModel == null)
            {
                return NotFound();
            }
            //Map Domain Model To DTO
            return Ok(mapper.Map<ProductDto>(productDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)
        {
            var deltedProductDomainModel=await productRepository.DeleteAsync(id);
            if (deltedProductDomainModel == null)
            {
                return NotFound();
            }
            //Map Domain Model To DTO
            return Ok(mapper.Map<ProductDto>(deltedProductDomainModel));
        }

    }
}
