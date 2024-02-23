using Newtonsoft.Json;
using Portal_MovilEsales.Services.AsesorServices.ViewModels;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DatosCliente;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.PedidoAprobado;

namespace Portal_MovilEsales.Services.AsesorServices
{
    public class AsesorService : IAsesorService
    {
        public DatosCliente getDatosCliente(string token, string codigoSapCliente)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/DatosCliente");
            request.Headers.Add("Authorization", "Bearer "+token);
            var content = new StringContent("{\r\n    \"CodigoSAPCliente\": \""+codigoSapCliente+"\",\r\n    \"navegadorweb\": \"Microsoft Edge XXX\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            DatosCliente datosCliente;
            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);
                datosCliente = JsonConvert.DeserializeObject<DatosCliente>(jsonInfo);
            }
            else
            {
                datosCliente = new DatosCliente();
            }
            return datosCliente;

        }

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

        public PedidoAprobado getPedidoAprobado(string token, string codigoPedido)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/PedidoAprobado");
            request.Headers.Add("Authorization", "Bearer "+token);
            var content = new StringContent("{\r\n    \"CodigoPedido\": \""+codigoPedido+"\",\r\n    \"navegadorweb\": \"Microsoft Edge XXX\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            PedidoAprobado pedidoAprobado;
            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);
                pedidoAprobado = JsonConvert.DeserializeObject<PedidoAprobado>(jsonInfo);
            }
            else
            {
                pedidoAprobado = new PedidoAprobado();
            }
            return pedidoAprobado;

        }
    }
}
