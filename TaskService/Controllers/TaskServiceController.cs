using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaskService.BLL.Facade;
using TaskService.BLL.Model;

namespace TaskService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComputeController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory; // for making requests to Database Microservice
        private readonly IConfiguration _configuration;

        public ComputeController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [HttpGet("Hi")]
        public async Task<IActionResult> getHi()
        {
            var apiUrl = _configuration.GetSection("ApiSettings:BaseUniqueStringUrl").Value;
            var httpClient = _clientFactory.CreateClient("YourNamedClient");
            string databaseServiceUrl = $"{apiUrl}/api/DatabaseService/Hi";
            var response = await httpClient.GetAsync(databaseServiceUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return Ok(jsonString);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to retrieve unique strings");
            }
        }

        [HttpGet("all-unique-strings")]
        public async Task<IActionResult> GetAllUniqueStrings()
        {
            var apiUrl = _configuration.GetSection("ApiSettings:BaseUniqueStringUrl").Value;
            var httpClient = _clientFactory.CreateClient("YourNamedClient");
            string databaseServiceUrl = $"{apiUrl}/api/DatabaseService/all-unique-strings";
            var response = await httpClient.GetAsync(databaseServiceUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var uniqueStrings = JsonConvert.DeserializeObject<List<string>>(jsonString);

                return Ok(uniqueStrings);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to retrieve unique strings");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateUnique(int count)
        {
            UniqueGenerator gen = new UniqueGenerator();
            UniqueStringGenerator gen2 = new UniqueStringGenerator();

            var stopwatch = Stopwatch.StartNew();
            //List<string> uniqueString = gen2.GenerateUniqueStrings(count, 5);
            List<string> uniqueString =  gen.GenerateUniqueStrings(count);
            await SendToDatabaseMicroservice(uniqueString);

            // Perform your task here
            stopwatch.Stop();

            // Respond back with the time taken
            return Ok(new { TimeTakenMilliseconds = stopwatch.ElapsedMilliseconds });
        }

        private async Task SendToDatabaseMicroservice(List<string> uniqueStrings)
        {
            var apiUrl = _configuration.GetSection("ApiSettings:BaseUniqueStringUrl").Value;
            var httpClient = _clientFactory.CreateClient("YourNamedClient");
            string databaseServiceUrl = $"{apiUrl}/api/DatabaseService";
            var content = new StringContent(JsonConvert.SerializeObject(uniqueStrings), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(databaseServiceUrl, content);

            if (!response.IsSuccessStatusCode)
            {
            }
        }
    }
}


