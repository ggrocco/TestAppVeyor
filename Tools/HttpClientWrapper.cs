using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tools
{
    public static class HttpClientWrapper
    {
        private const int TIMEOUT = 5; // 5 sec.

        // private static readonly ILog LOGGER = LogManager.GetLogger(typeof(HttpClientWrapper));

        public static async Task<ResponseContent> SendAsync(string url, int timeoutSenconds = TIMEOUT)
        {
            if (String.IsNullOrEmpty(url))
                return new ResponseContent { Body = "Invalid url", StatusCode = HttpStatusCode.NotFound };

            try
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(timeoutSenconds) })
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                    {
                        var result = await client.SendAsync(request);
                        var body = await result.Content.ReadAsStringAsync();
                        var statusCode = result.StatusCode;
                        result.Dispose();

                        return new ResponseContent
                        {
                            Body = body,
                            StatusCode = statusCode
                        };
                    }
                }
            }
            catch (Exception err)
            {
                //LOGGER.ErrorFormat(err, "Error on requesto to {0}", url);
                Console.WriteLine("Fail");
                return new ResponseContent(err);
            }
        }

        public class ResponseContent
        {
            public ResponseContent()
            {
            }

            public ResponseContent(Exception err)
            {
                this.Body = err.Message;
                this.StatusCode = HttpStatusCode.InternalServerError;
            }


            public string Body { get; set; }

            public HttpStatusCode StatusCode { get; set; }
        }
    }
}
