using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public interface IBannerzApiClient
    {
        BannerDTO GetAsync(int id);

        BannerDTO PostAsync(BannerDTO banner);

        BannerDTO PutAsync(int id, BannerDTO banner);

        BannerDTO DeleteAsync(int id);
    }

    public class BannerzApiClient : IBannerzApiClient
    {
        private HttpClient _client;

        public BannerzApiClient(string baseUrl)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
        }

        BannerDTO IBannerzApiClient.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        BannerDTO IBannerzApiClient.GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        BannerDTO IBannerzApiClient.PostAsync(BannerDTO banner)
        {
            throw new NotImplementedException();
        }

        BannerDTO IBannerzApiClient.PutAsync(int id, BannerDTO banner)
        {
            throw new NotImplementedException();
        }
    }
}