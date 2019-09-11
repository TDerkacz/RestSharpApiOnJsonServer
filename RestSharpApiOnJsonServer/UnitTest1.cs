using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace RestSharpApiOnJsonServer
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var client = new RestClient("http://localhost:3000");

            var request = new RestRequest("post/{postid}", Method.GET);
            // refer to resource
            request.AddUrlSegment("postid", 1);

            var content = client.Execute(request).Content;
            // response is a string

        }
    }
}
