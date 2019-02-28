using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LearningProject.Web
{
    public static class HttpExtensions
    {
        public static readonly JsonSerializer Serializer = new JsonSerializer { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        public static async Task WriteJson<T>(this HttpResponse response, T obj)
        {
            response.ContentType = "application/json";
                using (var writer = new HttpResponseStreamWriter(response.Body, Encoding.UTF8))
                {
                    using (var jsonWriter = new JsonTextWriter(writer))
                    {
                        jsonWriter.CloseOutput = false;

                        jsonWriter.AutoCompleteOnClose = false;

                        Serializer.Serialize(jsonWriter, obj);
                    }

                    await writer.FlushAsync();
                }
        }
    }
}
