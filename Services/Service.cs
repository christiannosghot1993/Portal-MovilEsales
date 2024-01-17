using Newtonsoft.Json;
using Portal_MovilEsales.Models;

namespace Portal_MovilEsales.Services
{
    public class Service : IService
    {
        public dynamic iniciarSesion(IniciarSesionRequest datosLogin)
        {
            var client=new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/loginweb");
            var content = new StringContent(JsonConvert.SerializeObject(datosLogin), null, "application/json");
            request.Content = content;
            var responseStatus = client.Send(request);
            string resultado= responseStatus.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<dynamic>(resultado);
        }
    }
}
