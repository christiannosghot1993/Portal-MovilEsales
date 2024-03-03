using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using Portal_MovilEsales.Services.AsesorServices;
using Portal_MovilEsales.Services.AsesorServices.ViewModels;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DatosCliente;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido.SimulacionPedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.PedidoAprobado;
using System.Drawing.Printing;

namespace Portal_MovilEsales.Controllers
{
    [Authorize]
    public class AsesorController : Controller
    {
        private IAsesorService _asesorService;
        public AsesorController(IAsesorService asesorService)
        {
            _asesorService = asesorService;
        }
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("token");
            var respInicioAsesor = _asesorService.getInfoInicioAsesor(token);
            var respDatosCliente = _asesorService.getDatosCliente(token, "0000090208");
            HttpContext.Session.SetString("contactoWhatsApp", "https://wa.me/" + respInicioAsesor.contactoWhatsApp);
            return View(respInicioAsesor);
        }

        public IActionResult PedidoCatalogoProductos(string? codigoSAPCliente = null)
        {
            var listaProductosSeleccionados = new List<ProductosNuevoPedido>();
            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listaProductosSeleccionados));
            var token = HttpContext.Session.GetString("token");

            var nuevoPedido = new NuevoPedido();

            if (codigoSAPCliente is not null)
            {
                nuevoPedido.cargaCabeceraPedido = _asesorService.getCargaCabeceraPedido(token, codigoSAPCliente);
            }

            var respListaFamiliasProductos = _asesorService.getFamiliaProductos(token);

            var respListaProductosFavoritos = _asesorService.getProductosFavoritos(token, "0000090208");

            nuevoPedido.listaFamiliaProductos = respListaFamiliasProductos;

            nuevoPedido.listadProductosFavoritos = respListaProductosFavoritos;

            return View(nuevoPedido);
        }


        public IActionResult FillCabeceraNuevoPedido(string codigoSAPCliente)
        {
            var token = HttpContext.Session.GetString("token");

            var nuevoPedido = new NuevoPedido();

            var respCargaCabeceraPedido = _asesorService.getCargaCabeceraPedido(token, codigoSAPCliente);

            nuevoPedido.cargaCabeceraPedido = respCargaCabeceraPedido;

            return PartialView("_FormCabeceraNuevoPedido", nuevoPedido);
        }

        public IActionResult GetProductosPorFamilia(string familia)
        {
            var token = HttpContext.Session.GetString("token");

            var nuevoPedido = new NuevoPedido();

            var respProductosPorFamilia = _asesorService.getProductosPorFamilia(token, familia);

            nuevoPedido.listaProductosPorFamilia = respProductosPorFamilia;

            return PartialView("_DetalleNuevoPedidoPorFamilia", nuevoPedido);
        }

        public IActionResult GetSimulacionPedido(
                string? codigoSAPCliente = null,
                string? codigoTipoEntrega = null,
                string? codigoTipoPago = null,
                string? codigoSAPDireccionEntrega = null,
                string? canal = null)
        {
            var token = HttpContext.Session.GetString("token");

            var jsonListaProductos = HttpContext.Session.GetString("SelectedProducts");

            var listaProductos = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(jsonListaProductos);

            var parametrosPeticion = JsonConvert.SerializeObject(new
            {
                CodigoSAPCliente = codigoSAPCliente,
                CodigoTipoEntrega = codigoTipoEntrega,
                CodigoTipoPago = codigoTipoPago,
                CodigoSAPDireccionEntrega = codigoSAPDireccionEntrega,
                Canal = canal,
                detallePedido = listaProductos.Select((producto) => new
                {
                    CodigoSAPArticulo = producto.codigo,
                    Cantidad = producto.cantidad,
                    Unidad = producto.unidad,
                    Bodega = "QU00",
                    DescFactura = Convert.ToDouble(producto.descFac),
                    DescNotaCredito = Convert.ToDouble(producto.descNc)
                })

            });

            var nuevoPedido = new NuevoPedido();

            var productosNuevoPedido = new List<ProductosNuevoPedido>();

            //var parametrosPeticion = "{\r\n    \"CodigoSAPCliente\": \"0000091390\",\r\n    \"CodigoTipoEntrega\": \"C\",\r\n    \"CodigoTipoPago\": \"I010\",\r\n    \"CodigoSAPDireccionEntrega\": \"00091390\",\r\n    \"Canal\": \"1\",\r\n    \"detallePedido\": [\r\n        {\r\n            \"CodigoSAPArticulo\": \"187940\",\r\n            \"Cantidad\": 10,\r\n            \"Unidad\": \"ST\",\r\n            \"Bodega\": \"QU00\",\r\n            \"DescFactura\": 0.00,\r\n            \"DescNotaCredito\": 0.00\r\n        }\r\n    ]\r\n}";

            var respSimulacionPedido = _asesorService.getSimulacionPedido(token, parametrosPeticion);

            var respCargaCabeceraPedido = _asesorService.getCargaCabeceraPedido(token, codigoSAPCliente);

            respSimulacionPedido.detallePedido.ForEach((producto) =>
            {
                var productoPorAgregar = new ProductosNuevoPedido()
                {
                    codigo = producto.codigoSAP,
                    descripcion = producto.nombreProducto,
                    unidad = producto.unidad,
                    peso = producto.peso,
                    subtotal = producto.subtotal,
                    descFac = producto.descFactura,
                    descNc = producto.descNotaCredito,
                    cantidad = producto.cantidad,
                    listadoTipoEntregas = respCargaCabeceraPedido.listadoTipoEntrega
                };

                productosNuevoPedido.Add(productoPorAgregar);
            });

            var resumenDeatalleProductos = new ResumenDetalleProductos()
            {
                descuentoBase = respSimulacionPedido.descuentoBase,
                descuentoPago = respSimulacionPedido.descuentoPago,
                descuentoPeso = respSimulacionPedido.descuentoPeso,
                descuentoRetiro = respSimulacionPedido.descuentoRetiro,
                descuentoVarios = respSimulacionPedido.descuentoVarios,
                importeBruto = respSimulacionPedido.importeBruto,
                iva = respSimulacionPedido.iva,
                margenPor = respSimulacionPedido.margenPor,
                seguroTransporte = respSimulacionPedido.seguroTransporte,
                subTotal1 = respSimulacionPedido.subTotal1,
                subTotal2 = respSimulacionPedido.subTotal2,
                valorNcsinIva = respSimulacionPedido.valorNcsinIva,
                valorTotal = respSimulacionPedido.valorTotal
            };

            nuevoPedido.listadoProductosNuevoPedido = productosNuevoPedido;

            nuevoPedido.resumenDetalleProductos = resumenDeatalleProductos;

            nuevoPedido.mensajeProceso = "fwef";

            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(productosNuevoPedido));

            HttpContext.Session.SetString("SummaryProducts", JsonConvert.SerializeObject(resumenDeatalleProductos));

            return PartialView("_TableProductosSeleccionadosPedido", nuevoPedido);
        }

        public IActionResult PostGuardarBorrador(
                string? codigoSAPCliente = null,
                string? codigoTipoEntrega = null,
                string? codigoTipoPago = null,
                string? codigoSAPDireccionEntrega = null,
                string? canal = null)
        {
            var token = HttpContext.Session.GetString("token");

            var jsonListaProductos = HttpContext.Session.GetString("SelectedProducts");

            var listaProductos = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(jsonListaProductos);

            var parametrosPeticion = JsonConvert.SerializeObject(new
            {
                CodigoSAPCliente = codigoSAPCliente,
                CodigoTipoEntrega = codigoTipoEntrega,
                CodigoTipoPago = codigoTipoPago,
                CodigoSAPDireccionEntrega = codigoSAPDireccionEntrega,
                Canal = canal,
                detallePedido = listaProductos.Select((producto) => new
                {
                    CodigoSAPArticulo = producto.codigo,
                    Cantidad = producto.cantidad,
                    Unidad = producto.unidad,
                    Bodega = "QU00",
                    DescFactura = Convert.ToDouble(producto.descFac),
                    DescNotaCredito = Convert.ToDouble(producto.descNc)
                })

            });

            var respGuardarBorrador = _asesorService.postGuardarPedidoBorrador(token, parametrosPeticion);

            var listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            var resumenDetalleProductos = JsonConvert.DeserializeObject<ResumenDetalleProductos>(HttpContext.Session.GetString("SummaryProducts")) ?? new ResumenDetalleProductos();

            var data = new
            {
                listadoProductosNuevoPedido,

                resumenDetalleProductos
            };

            return PartialView("_TableProductosSeleccionadosPedido", data);
        }

        public IActionResult PostNuevoPedido(
                string? codigoSAPCliente = null,
                string? codigoTipoEntrega = null,
                string? codigoTipoPago = null,
                string? codigoSAPDireccionEntrega = null,
                string? canal = null,
                string? fechaEntrega = null,
                string? numeroOrdenCompra = null,
                string? lugarEntrega = null,
                string? observaciones = null,
                string? contactoEntrega = null)
        {
            var token = HttpContext.Session.GetString("token");

            var jsonListaProductos = HttpContext.Session.GetString("SelectedProducts");

            var listaProductos = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(jsonListaProductos);

            var parametrosPeticion = JsonConvert.SerializeObject(new
            {
                CodigoSAPCliente = codigoSAPCliente,
                CodigoTipoEntrega = codigoTipoEntrega,
                CodigoTipoPago = codigoTipoPago,
                CodigoSAPDireccionEntrega = codigoSAPDireccionEntrega,
                Canal = canal,
                FechaEntrega = fechaEntrega,
                NumeroOrdenCompra = numeroOrdenCompra,
                LugarEntrega = lugarEntrega,
                Observaciones = observaciones,
                ContactoEntrega = contactoEntrega,
                detallePedido = listaProductos.Select((producto) => new
                {
                    CodigoSAPArticulo = producto.codigo,
                    Cantidad = producto.cantidad,
                    Unidad = producto.unidad,
                    Bodega = "QU00",
                    DescFactura = Convert.ToDouble(producto.descFac),
                    DescNotaCredito = Convert.ToDouble(producto.descNc),
                    AplicaFamilia = "S",
                    AplicaFinMes = "N"
                })

            });

            var respCrearNuevoPedido = _asesorService.postProcesoFlujoAprobacion(token, parametrosPeticion);

            var listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            var resumenDetalleProductos = JsonConvert.DeserializeObject<ResumenDetalleProductos>(HttpContext.Session.GetString("SummaryProducts")) ?? new ResumenDetalleProductos();

            //return View("PedidoCatalogoProductos");

            var mensajeProceso = respCrearNuevoPedido.mensajeRespuestaCalculoFlujo;

            ViewBag.MensajeProceso = mensajeProceso;

            var data = new
            {
                listadoProductosNuevoPedido,

                resumenDetalleProductos,
            };

            return PartialView("_TableProductosSeleccionadosPedido", data);

            //var mensajeRespuestaPeticion = respCrearNuevoPedido.mensajeRespuestaCalculoFlujo;

            //var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, "borrador", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"), "");

            //listadoPedidosBPH.procesoRespuesta.mensajeProceso = mensajeRespuestaPeticion;

            //return View("Pedidos", listadoPedidosBPH);
        }

        public IActionResult PoliticaComercial()
        {
            return View();
        }

        public IActionResult BuscarProductoCodigoSap(string codigoSapCliente, string codigoArticulo)
        {
            var token = HttpContext.Session.GetString("token");
            var resp = _asesorService.getProductoCodigoSap(token, codigoArticulo, codigoSapCliente);
            List<ProductosNuevoPedido> listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));
            listadoProductosNuevoPedido.Add(new ProductosNuevoPedido
            {
                bloqueado = false,
                codigo = resp.result.codigoSAPArticulo,
                descripcion = resp.result.nombre,
                listadoTipoEntregas = new List<ListadoTipoEntrega>(),
                unidad = resp.result.unidad,
                peso = resp.result.peso.ToString(),
                descFac = "0",
                descNc = "0",
                idl = "",
                subtotal = "",
                cantidad = "1",
                aFinMes = true,
                aFamilia = true
            });
            var data = new
            {
                listadoProductosNuevoPedido,

                resumenDetalleProductos = new ResumenDetalleProductos(),

                mensajeProceso = string.Empty
            };
            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listadoProductosNuevoPedido));
            return PartialView("_TableProductosSeleccionadosPedido", data);
        }

        public IActionResult PedidoProductosSeleccionados()
        {
            return View();
        }
        public IActionResult PedidoProductosSeleccionadosV2()
        {
            return View();
        }
        public IActionResult EstadoCuentaAsesor(string codigoSap)
        {
            var token = HttpContext.Session.GetString("token");

            var respEstadoCuenta = _asesorService.getInfoEstadoCuenta(token, codigoSap);

            return View(respEstadoCuenta);
        }

        public IActionResult Clientes()
        {
            var token = HttpContext.Session.GetString("token");
            var respInicioAsesor = _asesorService.getInfoInicioAsesor(token);
            List<ListadoClientes> lc = respInicioAsesor.listadoClientes;
            return View(lc);
        }

        public IActionResult InformacionCliente(string codigoSap)
        {
            var token = HttpContext.Session.GetString("token");
            var respDatosCliente = _asesorService.getDatosCliente(token, codigoSap);

            return View(respDatosCliente);
        }

        public IActionResult CargarInformacionModalPedidoAprobado(string numeroPedido)
        {
            var token = HttpContext.Session.GetString("token");
            DatosCliente dc = new DatosCliente();
            var respPedidoAprobado = _asesorService.getPedidoAprobado(token, numeroPedido);
            dc.pedidoAprobado = respPedidoAprobado;
            return PartialView("_ModalPedidoAprobado", dc);
        }

        public IActionResult CargarInformacionModalInformacionCrediticia(string codigoSAP)
        {
            var token = HttpContext.Session.GetString("token");

            var datosCliente = new DatosCliente();

            var respinformacionCrediticia = _asesorService.getInformacionCrediticia(token, codigoSAP);

            datosCliente.informacionCrediticia = respinformacionCrediticia;

            return PartialView("_ModalInformacionCrediticia", datosCliente);
        }

        public IActionResult CargarInformacionModalDetallePedidoEntregado(string numeroOrden)
        {
            var token = HttpContext.Session.GetString("token");

            var listadoPedidosBPH = new ListadoPedidosBPH();

            var respDetallePedido = _asesorService.getDetallePedido(token, numeroOrden);

            listadoPedidosBPH.detallePedidoInfo = respDetallePedido;

            return PartialView("_ModalDetallePedidoEntregado", listadoPedidosBPH);
        }

        public IActionResult CargarInformacionModalDetallePedidoAprobado(string numeroOrden)
        {
            var token = HttpContext.Session.GetString("token");

            var listadoPedidosBPH = new ListadoPedidosBPH();

            var respDetallePedido = _asesorService.getDetallePedido(token, numeroOrden);

            listadoPedidosBPH.detallePedidoInfo = respDetallePedido;

            return PartialView("_ModalDetallePedidoAprobado", listadoPedidosBPH);
        }

        public IActionResult Pedidos()
        {
            var token = HttpContext.Session.GetString("token");
            var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, "borrador", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"), "");
            return View(listadoPedidosBPH);
        }

        public IActionResult FillPedidosActivos()
        {
            var token = HttpContext.Session.GetString("token");
            var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, "pendiente", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"), "");
            return PartialView("_TablePedidosActivos", listadoPedidosBPH);
        }

        public IActionResult FillPedidosActivosData(DateTime fechaInicio, DateTime fechaFin, string cadena = "")
        {
            var token = HttpContext.Session.GetString("token");
            var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, "pendiente", fechaInicio, fechaFin, cadena);
            if (listadoPedidosBPH.result == null)
            {
                listadoPedidosBPH.result = new List<InfoPedidoBPH>();
            }
            return PartialView("_TablePedidosActivos", listadoPedidosBPH);
        }

        public IActionResult FillPedidosBorrador()
        {
            var token = HttpContext.Session.GetString("token");
            var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, "borrador", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"), "");
            return PartialView("_TablePedidosBorrador", listadoPedidosBPH);
        }

        public IActionResult FillPedidosBorradorData(DateTime fechaInicio, DateTime fechaFin, string cadena = "")
        {
            var token = HttpContext.Session.GetString("token");
            var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, "borrador", fechaInicio, fechaFin, cadena);
            if (listadoPedidosBPH.result == null)
            {
                listadoPedidosBPH.result = new List<InfoPedidoBPH>();
            }
            return PartialView("_TablePedidosBorrador", listadoPedidosBPH);
        }

        public IActionResult FillPedidosHistoricos()
        {
            var token = HttpContext.Session.GetString("token");
            var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, "", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"), "");
            return PartialView("_TablePedidosHistoricos", listadoPedidosBPH);
        }

        public IActionResult FillPedidosHistoricosData(DateTime fechaInicio, DateTime fechaFin, string cadena = "", string tipoPedido = "")
        {
            var token = HttpContext.Session.GetString("token");
            var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, tipoPedido, fechaInicio, fechaFin, cadena);
            if (listadoPedidosBPH.result == null)
            {
                listadoPedidosBPH.result = new List<InfoPedidoBPH>();
            }
            return PartialView("_TablePedidosHistoricos", listadoPedidosBPH);
        }
    }
}
