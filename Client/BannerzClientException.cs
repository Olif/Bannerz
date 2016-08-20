using System;
using System.Runtime.Serialization;
using System.Web;

namespace Client
{
    public class BannerzClientException : HttpException
    {
        public BannerzClientException()
        {
        }

        public BannerzClientException(string message)
        {

        }

        public BannerzClientException(int httpCode, string message) : base(httpCode, message)
        {
        }
    }
}