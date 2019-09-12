using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace RestSharpApiOnJsonServer
{
    [TestFixture]
    public class UnitTestGetResponse
    {
        [Test]
        public void GetResponseDeserializedWithRestSharp()
        {
            IRestClient client = new RestClient("http://localhost:3000");

            RestRequest request = new RestRequest("post/{postid}", Method.GET);

            request.AddUrlSegment("postid", 1);

            var content = client.Execute(request).Content;

            IRestResponse response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);

            var result = output["key"];
            Assert.That(result, Is.EqualTo("NEW"), "Field is different than expected");
        }

        [Test]
        public void GetResponseDeserializedWithNewtonSoft()
        {
            IRestClient client = new RestClient("http://localhost:3000");

            RestRequest request = new RestRequest("post/{postid}", Method.GET);

            request.AddUrlSegment("postid", 1);

            var content = client.Execute(request).Content;

            IRestResponse response = client.Execute(request);

            JObject obj = JObject.Parse(response.Content);
            Assert.That(obj["key"].ToString(), Is.EqualTo("NEW"), "Field is different than expected");
        }
    }
}
