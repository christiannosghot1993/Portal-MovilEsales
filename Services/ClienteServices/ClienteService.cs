using Newtonsoft.Json;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.EstadoCuenta;
using Portal_MovilEsales.Services.ClienteServices.ViewModels;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.DetallePedido;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.FamilaProducto;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.Inicio;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.NuevoPedido;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.NuevoPedido.GuardarPedidoBorrador;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.NuevoPedido.ProcesoFrujoAprobacion;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.PedidoAprobado;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.Reclamo;
using System.Text;

namespace Portal_MovilEsales.Services.ClienteServices
{
    public class ClienteService : IClienteService
    {
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

        public ListadoPedidosBPH getListadoPedidosBPH(string token, string tipoPedido, DateTime fechaInicio, DateTime fechaFin, string cadena)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ListadoPedidosXCliente");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
                FechaInicio = fechaInicio.ToString("yyyy-MM-dd"),
                FechaFin = fechaFin.ToString("yyyy-MM-dd"),
                estado = tipoPedido,
                cadena
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

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

            var jsonBody = JsonConvert.SerializeObject(new
            {
                CodigoPedido = codigoPedido,
                navegadorweb = "Microsoft Edge XXX"
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

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

        public ListadoProductosFavoritos getProductosPorFamilia(string token, string familiaNombre)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ListaProductosXFamilia");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
                familia = familiaNombre,
                codigosapcliente = "0000090208"
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

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

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                procesoFlujoAprobacion = JsonConvert.DeserializeObject<ProcesoFlujoAprobacion>(jsonInfo);
            }
            else
            {
                procesoFlujoAprobacion = new ProcesoFlujoAprobacion();
            }
            return procesoFlujoAprobacion;
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

        public InicioReclamo getReclamosCliente(string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ListaReclamosCliente");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\r\n    \"navegadorweb\":\"Microsoft Edge XXX\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);
            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            InicioReclamo inicioReclamo;
            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);
                inicioReclamo = JsonConvert.DeserializeObject<InicioReclamo>(jsonInfo);
            }
            else
            {
                inicioReclamo = new InicioReclamo();
            }
            return inicioReclamo;
        }

        public NuevoReclamo getInfoNuevoReclamo(string token)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/DatosNuevoReclamo");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX"
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            NuevoReclamo nuevoReclamo;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                nuevoReclamo = JsonConvert.DeserializeObject<NuevoReclamo>(jsonInfo);
            }
            else
            {
                nuevoReclamo = new NuevoReclamo();
            }

            return nuevoReclamo;
        }

        private static string ConvertFileToBase64(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(fileBytes);
        }

        //public NuevoReclamo postNuevoReclamo(string token, string motivo, string factura, string producto, string referencia, string asunto, string observaciones, byte[] fotografiaMaterial, byte[] copiaFactura)
        public NuevoReclamo postNuevoReclamo(string token, string motivo, string factura, string producto, string referencia, string asunto, string observaciones, string fotografiaMaterial, string copiaFactura)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/GuardaReclamo");

            // string filePath = "ruta/del/archivo/a/enviar.txt";

            // Convertir el archivo a base64
            //string base64String = ConvertFileToBase64(filePath);

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
                Motivo = motivo,
                Factura = factura,
                Producto = producto,
                Referencia = referencia,
                Asunto = asunto,
                Observaciones = observaciones,
                FotografiaMaterial = fotografiaMaterial,
                CopiaFactura = copiaFactura
                /*FotografiaMaterial = Convert.ToBase64String(fotografiaMaterial), // Convertir a Base64
                CopiaFactura = Convert.ToBase64String(copiaFactura)*/
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            NuevoReclamo nuevoReclamo;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                nuevoReclamo = JsonConvert.DeserializeObject<NuevoReclamo>(jsonInfo);
            }
            else
            {
                nuevoReclamo = new NuevoReclamo();
            }

            return nuevoReclamo;
        }

        public ReclamoEnProgreso postConfirmacionCliente(string token, int codigoReclamo, int calificacionServicio, string observaciones)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/ConfirmacionCliente");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
                CodigoReclamo = codigoReclamo,
                CalificacionServicio = calificacionServicio,
                Observaciones = observaciones
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            ReclamoEnProgreso reclamoEnProgreso;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                reclamoEnProgreso = JsonConvert.DeserializeObject<ReclamoEnProgreso>(jsonInfo);
            }
            else
            {
                reclamoEnProgreso = new ReclamoEnProgreso();
            }

            return reclamoEnProgreso;
        }

        public ReclamoEnProgreso getDetalleReclamoCliente(string token, int codigoReclamo)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/DetalleReclamoCliente");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
                CodigoReclamo = codigoReclamo
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            ReclamoEnProgreso reclamoEnProgreso;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                reclamoEnProgreso = JsonConvert.DeserializeObject<ReclamoEnProgreso>(jsonInfo);
            }
            else
            {
                reclamoEnProgreso = new ReclamoEnProgreso();
            }

            return reclamoEnProgreso;
        }

    }
}
