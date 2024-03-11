using Newtonsoft.Json;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.Inicio;
using System.Text;

namespace Portal_MovilEsales.Services.ClienteServices
{
    public class ClienteService : IClienteService
    {
        public InicioCliente getInfoInicioCliente(string token)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/InicioCliente");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                //CodigoSAPCliente = codigoSAPCliente,
                navegadorweb = "Microsoft Edge XXX"
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            InicioCliente inicioCliente;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                inicioCliente = JsonConvert.DeserializeObject<InicioCliente>(jsonInfo);
            }
            else
            {
                inicioCliente = new InicioCliente();
            }

            return inicioCliente;
        }

        public InformacionCrediticia getInformacionCrediticia(string token, string codigoSAPCliente)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/InformacionCrediticia");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                CodigoSAPCliente = codigoSAPCliente,
                navegadorweb = "Microsoft Edge XXX"
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            InformacionCrediticia informacionCrediticia;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                informacionCrediticia = JsonConvert.DeserializeObject<InformacionCrediticia>(jsonInfo);
            }
            else
            {
                informacionCrediticia = new InformacionCrediticia();
            }

            return informacionCrediticia;
        }
    }
}
