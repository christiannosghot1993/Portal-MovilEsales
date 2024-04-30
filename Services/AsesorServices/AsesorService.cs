using Newtonsoft.Json;
using Portal_MovilEsales.Services.AsesorServices.ViewModels;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DatosCliente;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DetallePedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.EstadoCuenta;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.FamilaProducto;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido.GuardarPedidoBorrador;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido.ProcesoFrujoAprobacion;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido.SimulacionPedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.PedidoAprobado;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.PoliticaComercial;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.ProductoExcel;
using System.Reflection.Metadata.Ecma335;
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

        //para buscar a los clientes por coincidencias SB Marzo 2024
        public InicioAsesor getInfoInicioAsesor(string token, string cadena)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/InicioAsesor");
            request.Headers.Add("Authorization", "Bearer " + token);
            //var content = new StringContent("{\r\n    \"navegadorweb\":\"Microsoft Edge XXX\"\r\n}", null, "application/json");
            var content = new StringContent("{\r\n    \"cadena\": \"" + cadena + "\",\r\n    \"navegadorweb\":\"Microsoft Edge XXX\"\r\n}", null, "application/json");
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

        public ListadoPedidosBPH getListadoPedidosBPH(string token, string tipoPedido, DateTime fechaInicio, DateTime fechaFin, string cadena)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ListadoPedidos");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\r\n    \"navegadorweb\": \"Microsoft Edge XXX\",\r\n    \"FechaInicio\": \"" + fechaInicio.ToString("yyyy-MM-dd") + "\",\r\n    \"FechaFin\": \"" + fechaFin.ToString("yyyy-MM-dd") + "\",\r\n    \"estado\": \"" + tipoPedido + "\",\r\n    \"cadena\": \"" + cadena + "\"\r\n}", null, "application/json");

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
                pedidoAprobado.observacion = "Error " + resDynamic.resultcode + " - " + resDynamic.message;
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
                listaFamiliaProductos = new();
            }

            return listaFamiliaProductos;
        }

        public ListadoProductosFavoritos getProductosPorFamilia(string token, string familiaNombre, string codigoSAPCliente)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ListaProductosXFamilia");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
                familia = familiaNombre,
                codigosapcliente = codigoSAPCliente
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            //resultado = resultado.Replace("base", "baseProducto");

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            ListadoProductosFavoritos listaProductos;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic);

                listaProductos = JsonConvert.DeserializeObject<ListadoProductosFavoritos>(jsonInfo);
            }
            else
            {
                listaProductos = new();
            }

            return listaProductos;
        }

        public ListadoProductosFavoritos getProductosFavoritos(string token, string codigoCliente)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ListaProductosXFavoritos");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\r\n    \"navegadorweb\": \"Microsoft Edge XXX\",\r\n    \"codigosapcliente\": \"" + codigoCliente + "\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);


            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            ListadoProductosFavoritos productosFavoritos;
            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic);
                productosFavoritos = JsonConvert.DeserializeObject<ListadoProductosFavoritos>(jsonInfo);
            }
            else
            {
                productosFavoritos = new ListadoProductosFavoritos();
            }
            return productosFavoritos;
        }

        public ProductoCodigoSap getProductoCodigoSap(string token, string codigoArticulo, string codigoSapCliente)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ProductoXCodigoSAP");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\r\n    \"navegadorweb\": \"Microsoft Edge XXX\",\r\n    \"codigoarticulo\": \"" + codigoArticulo + "\",\r\n    \"codigosapcliente\": \"" + codigoSapCliente + "\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            ProductoCodigoSap productosCodigoSap;
            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic);
                productosCodigoSap = JsonConvert.DeserializeObject<ProductoCodigoSap>(jsonInfo);
            }
            else
            {
                productosCodigoSap = new ProductoCodigoSap();
            }
            return productosCodigoSap;

        }

        public SimulacionPedido getSimulacionPedido(string token, string parametrosPeticion)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/SimulacionPedido");

            request.Headers.Add("Authorization", "Bearer " + token);

            var content = new StringContent(parametrosPeticion, null, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            SimulacionPedido simulacionPedido;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                simulacionPedido = JsonConvert.DeserializeObject<SimulacionPedido>(jsonInfo);
            }
            else
            {
                simulacionPedido = new SimulacionPedido();
            }
            return simulacionPedido;
        }

        public GuardarPedidoBorrador postGuardarPedidoBorrador(string token, string parametrosPeticion)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/GuardaPedidoBorrador");

            request.Headers.Add("Authorization", "Bearer " + token);

            var content = new StringContent(parametrosPeticion, null, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            GuardarPedidoBorrador pedidoBorrador;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                pedidoBorrador = JsonConvert.DeserializeObject<GuardarPedidoBorrador>(jsonInfo);
            }
            else
            {
                pedidoBorrador = new GuardarPedidoBorrador();
            }
            return pedidoBorrador;
        }

        public ProcesoFlujoAprobacion postProcesoFlujoAprobacion(string token, string parametrosPeticion)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ProcesaFlujoAprobacion");

            request.Headers.Add("Authorization", "Bearer " + token);

            var content = new StringContent(parametrosPeticion, null, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            ProcesoFlujoAprobacion procesoFlujoAprobacion;

            //if ((bool)resDynamic.success)
            //{
            string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);
            jsonInfo = jsonInfo.Replace("mensaje", "mensajeRespuestaCalculoFlujo");
            procesoFlujoAprobacion = JsonConvert.DeserializeObject<ProcesoFlujoAprobacion>(jsonInfo);
            //}
            //else
            //{
            //    procesoFlujoAprobacion = new ProcesoFlujoAprobacion();
            //}
            return procesoFlujoAprobacion;
        }

        public ProductoExcel getProductosExcel(string token, ProductoExcelRequest listadoProductos)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ValidaExcel");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent(JsonConvert.SerializeObject(listadoProductos), null, "application/json");
            request.Content = content;
            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            ProductoExcel productosExcel;
            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic);
                productosExcel = JsonConvert.DeserializeObject<ProductoExcel>(jsonInfo);
            }
            else
            {
                productosExcel = new ProductoExcel();
            }
            return productosExcel;
        }

        public List<StockArticulo> getStockArticulos(string token, string codigoSap)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ConsultaStock");
            request.Headers.Add("Authorization", "Bearer "+token);
            var content = new StringContent("{\r\n   \"navegadorweb\":\"Microsoft Edge XXX\",\r\n   \"codigosaparticulo\":\""+codigoSap+"\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            string jsonStock = resDynamic.result+"";
            List<StockArticulo> dataStocks = JsonConvert.DeserializeObject<List<StockArticulo>>(jsonStock);
            return dataStocks;
        }

        public string savePoliticaComercial(string token, DetallePolitica detallePolitica)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ValidaPolitica");
            request.Headers.Add("Authorization", "Bearer "+token);
            var content = new StringContent(JsonConvert.SerializeObject(detallePolitica), null, "application/json");
            request.Content = content;
            var responseStatus = client.Send(request);
            string resultado = responseStatus.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(resultado))
            {
                resultado = "Se produjo un error al guardar la poítica comercial";
            }
            var dRes = JsonConvert.DeserializeObject<dynamic>(resultado);
            resultado = dRes.message;
            return resultado;
        }

        public List<LecturaPoliticaComercial> getPoliticaComercial(string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/PoliticaVigente");
            request.Headers.Add("Authorization", "Bearer "+token);
            var content = new StringContent("{\r\n    \"navegadorweb\": \"Microsoft Edge XXX\"\r\n}\r\n", null, "application/json");
            request.Content = content;
            var responseStatus = client.Send(request);
            string resultado = responseStatus.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            if ((bool)resDynamic.success)
            {
                var jsonPC = JsonConvert.SerializeObject(resDynamic.result);
                List<LecturaPoliticaComercial> lpc = JsonConvert.DeserializeObject<List<LecturaPoliticaComercial>>(jsonPC);
                return lpc;
            }
            else
            {
                return new List<LecturaPoliticaComercial>();
            }
            
        }
    }
}
