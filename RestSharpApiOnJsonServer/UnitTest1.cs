using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace RestSharpApiOnJsonServer
{
    // [TestClass] MSTest
    [TestFixture] // NUnit
    public class UnitTestGetResponse
    {
        // [TestMethod] MSTest
        [Test] // NUnit
        public void GetResponseDeserializeWithRestSharp()
        {
            IRestClient client = new RestClient("http://localhost:3000");

            RestRequest request = new RestRequest("post/{postid}", Method.GET);
            // in refer to resource post

            request.AddUrlSegment("postid", 1);
            // in refer to resource post/1

            var content = client.Execute(request).Content;
            // content is a string

            IRestResponse response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);

            var result = output["key"];
            Assert.That(result, Is.EqualTo("NEW"), "Field is different than expected");


            // ---> add package
            // NUnit

            // ---> remove MSTest packages and code

            // Microsoft.VisualStudio.Test
            // Microsoft.VisualStudio.TestTools.UnitTesting.Assert();

            // MSTest.TestAdapter
            // MSTest.TestFramework
            // [TestClass] MSTest
            // [TestMethod] MSTest
        }
    }
}
