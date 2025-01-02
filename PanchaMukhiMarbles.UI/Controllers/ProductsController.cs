using Microsoft.AspNetCore.Mvc;
using PanchaMukhiMarbles.UI.Models;
using PanchaMukhiMarbles.UI.Models.DTO;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace PanchaMukhiMarbles.UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task <IActionResult> Index()
        {
            List<ProductDto> response = new List<ProductDto>();
            try
            {
                //Get All Products From Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7108/api/products");

                httpResponseMessage.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>());
               
            }
            catch (Exception ex)
            {
                //Log The Exception
                throw;
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage =new HttpRequestMessage()
            {
                Method=HttpMethod.Post,
                RequestUri=new Uri ("https://localhost:7108/api/products"),
                Content =new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8,"application/json")
            };

            var httpResponseMessage=await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response=await httpResponseMessage.Content.ReadFromJsonAsync<ProductDto>();
            if (response != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client=httpClientFactory.CreateClient();
            var response=await client.GetFromJsonAsync<ProductDto>($"https://localhost:7108/api/products/{id.ToString()}");
            if (response != null)
            {
                return View(response);
            }
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Edit(ProductDto request)
        {
            var client=httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7108/api/products/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage=await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<ProductDto>();
            if (response != null)
            {
                return RedirectToAction("Edit","Products");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7108/api/products/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Products");
            }
            catch (Exception ex)
            {
                //Console
            }
            return View("Edit");
        }
    }
}
