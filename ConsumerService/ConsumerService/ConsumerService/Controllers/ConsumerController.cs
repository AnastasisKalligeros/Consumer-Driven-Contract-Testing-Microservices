using Microsoft.AspNetCore.Mvc;

namespace ConsumerService.Controllers
{
    [ApiController]
    [Route("api/consumer")]
    public class ConsumerController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ConsumerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetFromProvider()
        {
            var response = await _httpClient.GetStringAsync("http://localhost:5001/api/data");
            return Ok(response);
        }
    }

}
