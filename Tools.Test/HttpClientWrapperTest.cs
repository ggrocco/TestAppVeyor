using System;
using Xunit;

namespace Tools.Test
{
    public class HttpClientWrapperTest
    {
        [Fact]
        public void HttpClientWrapperendSendAsyncGoogle()
        {
            var request = HttpClientWrapper.SendAsync("https://www.google.com").Result;
            Assert.Contains("<title>Google</title>", request.Body, StringComparison.OrdinalIgnoreCase);
        }
    }
}
