using Domain;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public interface IBannerzApiClient
    {
        Task<BannerDTO> GetAsync(int id);

        Task<BannerDTO> CreateAsync(BannerDTO banner);

        Task<BannerDTO> UpdateAsync(int id, BannerDTO banner);

        Task<BannerDTO> DeleteAsync(int id);
    }

    public class BannerzApiClient : IBannerzApiClient
    {
        private HttpClient _client;

        public BannerzApiClient(string baseUrl)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<BannerDTO> DeleteAsync(int id)
        {
            var path = $"api/v1/banners/{id}";
            var response = await _client.DeleteAsync(path);
            return await HandleResponse(response);
        }

        public async Task<BannerDTO> GetAsync(int id)
        {
            var path = $"api/v1/banners/{id}";
            var response = await _client.GetAsync(path);
            return await HandleResponse(response);
        }

        public async Task<BannerDTO> CreateAsync(BannerDTO banner)
        {
            var path = $"api/v1/banners";
            StringContent content = CreateJsonContent(banner);
            var response = await _client.PostAsync(path, content);
            return await HandleResponse(response);
        }

        public async Task<BannerDTO> UpdateAsync(int id, BannerDTO banner)
        {
            var path = $"api/v1/banners/{id}";
            var content = CreateJsonContent(banner);
            var response = await _client.PutAsync(path, content);
            return await HandleResponse(response);
        }

        private static StringContent CreateJsonContent(BannerDTO banner)
        {
            var json = JsonConvert.SerializeObject(banner);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }

        private static async Task<BannerDTO> HandleResponse(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                throw new BannerzClientException((int)response.StatusCode, errorMsg);
            }
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BannerDTO>(json);
        }
    }
}