using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Api.Domain
{
    public class W3CHtmlValidator : IHtmlValidator
    {
        private static Uri baseUri = new Uri("https://validator.w3.org/nu/");

        HttpClient _httpClient;

        public W3CHtmlValidator()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUri;
        }

        public async Task<HtmlValidationResult> ValidateAsync(string html)
        {
            var content = new StringContent(html, Encoding.UTF8, "text/html");
            var response = await _httpClient.PostAsync("?out=json", content);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpException("Could not contact w3c");
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseContent);
            var errors = json["messages"]
                .Where(x => (string)x["type"] == "error")
                .Select(x => $@"Error: [{x["lastLine"]}] - {x["message"]}");

            if (errors != null && errors.Count() > 0)
            {
                return HtmlValidationResult.ErrorResult(errors);
            }
            else
            {
                return HtmlValidationResult.ValidResult;
            }
        }
    }
}