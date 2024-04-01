using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnimeNet.Service
{
    public class ApiService
    {

        private readonly HttpClient _httpClient;

        public ApiService()
        {

            _httpClient = new HttpClient();
        }

        public async Task<string> GetDataFromApi(string apiUrl)
        {

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(apiUrl);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {

                throw new Exception($"Error al llamar a la API. Código de estado: {response.StatusCode}");
            }
        }
    }
}
