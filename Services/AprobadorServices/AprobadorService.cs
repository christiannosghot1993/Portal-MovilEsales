using Newtonsoft.Json;
using Portal_MovilEsales.Services.AprobadorServices.ViewModels.DetallePedidoAprobador;
using Portal_MovilEsales.Services.AprobadorServices.ViewModels.InicioAprobador;
using Portal_MovilEsales.Services.AsesorServices.ViewModels;

namespace Portal_MovilEsales.Services.AprobadorServices
{
    public class AprobadorService : IAprobadorService
    {
        public DetallePedidoAprobador getDetallePedidoAprobador(string token, string numeroOrden)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/DetallePedidoAprobador");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
                NumeroOrden = numeroOrden
            });

            var content = new StringContent(jsonBody, null, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            DetallePedidoAprobador detallePedidoAprobador;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                detallePedidoAprobador = JsonConvert.DeserializeObject<DetallePedidoAprobador>(jsonInfo);
            }
            else
            {
                detallePedidoAprobador = new DetallePedidoAprobador();
            }

            return detallePedidoAprobador;
        }

        public InicioAprobador getInfoInicioAprobador(string token)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/InicioAprobador");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX"
            });

            var content = new StringContent(jsonBody, null, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            InicioAprobador inicioAprobador;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                inicioAprobador = JsonConvert.DeserializeObject<InicioAprobador>(jsonInfo);
            }
            else
            {
                inicioAprobador = new InicioAprobador();
            }

            return inicioAprobador;
        }
    }
}
