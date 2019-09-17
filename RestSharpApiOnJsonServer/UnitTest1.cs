using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using RestSharpApiOnJsonServer.Models;
using RestSharpApiOnJsonServer.Utils;
using System.Collections.Generic;
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
        public void GetResponseDeserializedWithNewtonSoftPackage()
        {
            IRestClient client = new RestClient("http://localhost:3000");

            RestRequest request = new RestRequest("post/{postid}", Method.GET);

            request.AddUrlSegment("postid", 1);

            var content = client.Execute(request).Content;

            IRestResponse response = client.Execute(request);

            //JObject obj = JObject.Parse(response.Content);
            //Assert.That(obj["key"].ToString(), Is.EqualTo("NEW"), "Field is different than expected");

            var result = response.GetValueFromJsonObject("key");
            Assert.That(result, Is.EqualTo("NEW"), "Field is different than expected");
        }

        [Test]
        public void PostWithAnonymousMethod()
        {
            IRestClient client = new RestClient("http://localhost:3000");

            RestRequest request = new RestRequest("post", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new {name = "John"});
            request.AddUrlSegment("postid", 1);

            IRestResponse response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);

            var result = output["name"];

            Assert.That(result, Is.EqualTo("John"), "Name is different than expected");
        }

        [Test]
        public void PostWithDataModel()
        {
            IRestClient client = new RestClient("http://localhost:3000");

            RestRequest request = new RestRequest("post", Method.POST);
            
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Post(){ Id = "5", Key = "CARS", Language = "EN_FI" });
            request.AddUrlSegment("postid", 1);

            IRestResponse response = client.Execute(request);

            var result = response.DeserializeResponse()["Key"];

            Assert.That(result, Is.EqualTo("CARS"), "Key value is different than expected");
        }

        [Test]
        public void PostAndDeserializedResponseByGenericTypeDataModel()
        {
            IRestClient client = new RestClient("http://localhost:3000");

            RestRequest request = new RestRequest("post", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Post() { Id = "4", Key = "CARS", Language = "EN_FI" });

            var response = client.Execute<Post>(request);

            Assert.That(response.Data.Key, Is.EqualTo("CARS"), "Key value is different than expected");
        }

        [Test]
        public void PostWithAsync()
        {
            RestClient client = new RestClient("http://localhost:3000");

            RestRequest request = new RestRequest("post", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Post() { Id = "5", Key = "CARS", Language = "EN_FI" });

            var response = client.Execute<Post>(request);

            var result = Library.ExecuteAsyncRequestTask<Post>(client, request).GetAwaiter().GetResult();
        }
    }
}
