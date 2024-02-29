using Newtonsoft.Json;
using Portal_MovilEsales.Services.AsesorServices.ViewModels;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DatosCliente;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DetallePedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.EstadoCuenta;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.FamilaProducto;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.PedidoAprobado;
using System.Text;

namespace Portal_MovilEsales.Services.AsesorServices
{
    public class AsesorService : IAsesorService
    {
        public DatosCliente getDatosCliente(string token, string codigoSapCliente)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/DatosCliente");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\r\n    \"CodigoSAPCliente\": \"" + codigoSapCliente + "\",\r\n    \"navegadorweb\": \"Microsoft Edge XXX\"\r\n}", null, "application/json");
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
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\r\n    \"navegadorweb\":\"Microsoft Edge XXX\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);
            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
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

        public ListadoPedidosBPH getListadoPedidosBPH(string token, string tipoPedido, DateTime fechaInicio, DateTime fechaFin)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ListadoPedidos");
            request.Headers.Add("Authorization", "Bearer "+ token);
            var content = new StringContent("{\r\n    \"navegadorweb\": \"Microsoft Edge XXX\",\r\n    \"FechaInicio\": \""+fechaInicio.ToString("yyyy-MM-dd")+"\",\r\n    \"FechaFin\": \""+fechaFin.ToString("yyyy-MM-dd")+"\",\r\n    \"estado\": \""+tipoPedido+"\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            ListadoPedidosBPH listadoPedidosBPH;
            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic);
                listadoPedidosBPH = JsonConvert.DeserializeObject<ListadoPedidosBPH>(jsonInfo);
            }
            else
            {
                listadoPedidosBPH = new ListadoPedidosBPH();
            }
            return listadoPedidosBPH;
        }

        public PedidoAprobado getPedidoAprobado(string token, string codigoPedido)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/PedidoAprobado");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\r\n    \"CodigoPedido\": \"" + codigoPedido + "\",\r\n    \"navegadorweb\": \"Microsoft Edge XXX\"\r\n}", null, "application/json");
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
                pedidoAprobado.observacion = "Error "+resDynamic.resultcode+" - "+resDynamic.message;
            }
            return pedidoAprobado;

        }

        public EstadoCuenta getInfoEstadoCuenta(string token, string codigoSAPCliente)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/EstadoDeCuenta");

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

            EstadoCuenta estadoCuenta;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                estadoCuenta = JsonConvert.DeserializeObject<EstadoCuenta>(jsonInfo);
            }
            else
            {
                estadoCuenta = new EstadoCuenta();
            }

            return estadoCuenta;
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

        public DetallePedidoInfo getDetallePedido(string token, string numeroOrden)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/DetallePedido");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
                NumeroOrden = numeroOrden
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            DetallePedidoInfo detallePedido;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                detallePedido = JsonConvert.DeserializeObject<DetallePedidoInfo>(jsonInfo);
            }
            else
            {
                detallePedido = new DetallePedidoInfo();
            }

            return detallePedido;
        }

        public CargaCabeceraPedido getCargaCabeceraPedido(string token, string codigoSAPCliente)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/CargaCabeceraPedido");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
                CodigoSAPCliente = codigoSAPCliente
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            CargaCabeceraPedido cargaCabeceraPedido;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                cargaCabeceraPedido = JsonConvert.DeserializeObject<CargaCabeceraPedido>(jsonInfo);
            }
            else
            {
                cargaCabeceraPedido = new CargaCabeceraPedido();
            }

            return cargaCabeceraPedido;
        }

        public List<FamiliaProducto> getFamiliaProductos(string token)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ListarFamiliaProductos");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            List<FamiliaProducto> listaFamiliaProductos;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                listaFamiliaProductos = JsonConvert.DeserializeObject<List<FamiliaProducto>>(jsonInfo);
            }
            else
            {
                listaFamiliaProductos = new ();
            }

            return listaFamiliaProductos;
        }
    }
}
