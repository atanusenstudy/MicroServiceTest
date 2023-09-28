using System;
using System.Text;

namespace Compute.Microservice.Helpers
{
    public static class ApiHelper
    {
        private static string url = "http://host.docker.internal:8066/api/Unique/PostAll";
        /// <summary>
        /// This method is responsible to call other microservice api to store unique code in db
        /// </summary>
        /// <param name="payload">payload is json string of UQID model list</param>
        public async static Task<int> ApiCall(string payload)
        {
            var data = new StringContent(payload, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(10);
            var response = await client.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();
            return 1;
        }
    }
}
