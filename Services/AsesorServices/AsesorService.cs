using Newtonsoft.Json;
using Portal_MovilEsales.Services.AsesorServices.ViewModels;

namespace Portal_MovilEsales.Services.AsesorServices
{
    public class AsesorService : IAsesorService
    {
        public InicioAsesor getInfoInicioAsesor(string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/InicioAsesor");
            request.Headers.Add("Authorization", "Bearer "+ token);
            var content = new StringContent("{\r\n    \"navegadorweb\":\"Microsoft Edge XXX\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);
            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic=JsonConvert.DeserializeObject<dynamic>(resultado);
            InicioAsesor inicioAsesor;
            if ((bool)resDynamic.success)
            {
               string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);
               inicioAsesor = JsonConvert.DeserializeObject<InicioAsesor>(jsonInfo);
            }
            else
            {
                inicioAsesor = new InicioAsesor();
            }
            return inicioAsesor;
        }
    }
}
