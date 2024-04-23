using Newtonsoft.Json;
using Portal_MovilEsales.Models;

namespace Portal_MovilEsales.Services
{
    public class Service : IService
    {
        public string changePassword(string token, Authentication auth)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/CambioContrasenia");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent(JsonConvert.SerializeObject(auth), null, "application/json");
            request.Content = content;
            var responseStatus = client.Send(request);
            string resultado = responseStatus.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(resultado))
            {
                resultado = "Se produjo un error al cambiar la contraseña.";
            }
            var dRes= JsonConvert.DeserializeObject<dynamic>(resultado);
            if ((bool)dRes.success) {
                resultado = dRes.result.mensaje;
            }
            return resultado;
        }

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
