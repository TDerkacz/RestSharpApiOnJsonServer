using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestSharpApiOnJsonServer.Utils
{
    public static class Library
    {
        public static async Task<IRestResponse<T>> ExecuteAsyncRequestTask<T>(this RestClient client, IRestRequest request)
           where T : class, new()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();

            client.ExecuteAsync<T>(request, restResponse =>
            {
                if (restResponse.ErrorException != null)
                {
                    const string message = "Error retrieving response";
                    throw new ApplicationException(message, restResponse.ErrorException);
                }

                taskCompletionSource.SetResult(restResponse);
            });

            return await taskCompletionSource.Task;
        }


        // response based on RestSharp and Dictionary stucture
        public static Dictionary<string, string> DeserializeResponse(this IRestResponse restResponse)
        {
            var deserialize = new JsonDeserializer();
            var jsonObject = deserialize.Deserialize<Dictionary<string, string>>(restResponse);
            return jsonObject;
        }

        // response based on NewtonSoft Json deserialisation
        public static string GetValueFromJsonObject(this IRestResponse response, string responseObject)
        {
            JObject obj = JObject.Parse(response.Content);
            return obj[responseObject].ToString();
        }
    }
}
