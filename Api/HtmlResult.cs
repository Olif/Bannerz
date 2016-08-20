using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Api
{
    public class HtmlResult : IHttpActionResult
    {
        private string _html;
        private HttpRequestMessage _request;

        public HtmlResult(string html, HttpRequestMessage request)
        {
            _html = html;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage() {
                Content = new StringContent(_html, Encoding.UTF8, "text/html"),
                RequestMessage = _request
            };

            return Task.FromResult(response);
        }
    }
}