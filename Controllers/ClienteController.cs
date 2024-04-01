using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using Portal_MovilEsales.Services.ClienteServices;
using Portal_MovilEsales.Services.ClienteServices.ViewModels;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.Inicio;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.NuevoPedido;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.Reclamo;

using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Portal_MovilEsales.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("token");

            var respInicioCliente = _clienteService.getInfoInicioCliente(token);

            var listaImagenes = respInicioCliente.listadoImagenes;

            HttpContext.Session.SetString("ListImages", JsonConvert.SerializeObject(listaImagenes));

            return View(respInicioCliente);
        }

        #region Nuevo Pedido

        public IActionResult NuevoPedido(string codigoSAPCliente)
        {
            var listaProductosSeleccionados = new List<ProductosNuevoPedido>();

            var resumenDetalleProductos = new ResumenDetalleProductos();

            var contadorRegistros = 1;

            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listaProductosSeleccionados));

            HttpContext.Session.SetString("SummaryProducts", JsonConvert.SerializeObject(resumenDetalleProductos));

            HttpContext.Session.SetString("RowCounter", contadorRegistros.ToString());

            var token = HttpContext.Session.GetString("token");

            var nuevoPedido = new NuevoPedido
            {
                cargaCabeceraPedido = _clienteService.getCargaCabeceraPedido(token, codigoSAPCliente)
            };

            var respListaFamiliasProductos = _clienteService.getFamiliaProductos(token);

            var respListaProductosFavoritos = _clienteService.getProductosFavoritos(token, codigoSAPCliente);

            nuevoPedido.listaFamiliaProductos = respListaFamiliasProductos;

            nuevoPedido.listadProductosFavoritos = respListaProductosFavoritos;

            return View(nuevoPedido);
        }

        public IActionResult GetProductosPorFamilia(string familia)
        {
            var token = HttpContext.Session.GetString("token");

            var nuevoPedido = new NuevoPedido();

            var respProductosPorFamilia = _clienteService.getProductosPorFamilia(token, familia);

            nuevoPedido.listaProductosPorFamilia = respProductosPorFamilia;

            return PartialView("_DetalleNuevoPedidoPorFamiliaCliente", nuevoPedido);
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
                    Bodega = producto.listadoTipoEntregas.Find(te => te.porDefecto is "S").codigoTipoEntrega,
                    DescFactura = Convert.ToDouble(producto.descFac),
                    DescNotaCredito = Convert.ToDouble(producto.descNc)
                })

            });

            var respGuardarBorrador = _clienteService.postGuardarPedidoBorrador(token, parametrosPeticion);

            var listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            var resumenDetalleProductos = JsonConvert.DeserializeObject<ResumenDetalleProductos>(HttpContext.Session.GetString("SummaryProducts")) ?? new ResumenDetalleProductos();

            var data = new
            {
                listadoProductosNuevoPedido,

                resumenDetalleProductos
            };

            return PartialView("_TableProductosSeleccionadosPedidoCliente", data);
        }

        public IActionResult SeleccionarArticuloPorFamilia(string cantidadArticulo, string codigoSAPArticulo, string familia)
        {
            var token = HttpContext.Session.GetString("token");

            var listProductosSeleccionados = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            var numeroContador = JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("RowCounter"));

            var listadoProductosPorFamilia = _clienteService.getProductosPorFamilia(token, familia);

            var productoPorAgregar = listadoProductosPorFamilia.result.FirstOrDefault(p => p.codigoSAPArticulo == codigoSAPArticulo);

            var respCargaCabeceraPedido = _clienteService.getCargaCabeceraPedido(token, "0000090208");

            listProductosSeleccionados.Add(new ProductosNuevoPedido
            {
                numeroRegistro = numeroContador.ToString(),
                codigo = productoPorAgregar.codigoSAPArticulo,
                descripcion = productoPorAgregar.nombre,
                listadoTipoEntregas = respCargaCabeceraPedido.listadoTipoEntrega,
                unidad = productoPorAgregar.unidad,
                peso = productoPorAgregar.peso.ToString(),
                descFac = productoPorAgregar.descFactura.ToString(),
                descNc = productoPorAgregar.descNC.ToString(),
                idl = "",
                subtotal = "",
                cantidad = cantidadArticulo,
            });

            var data = new
            {
                listadoProductosNuevoPedido = listProductosSeleccionados,

                resumenDetalleProductos = new ResumenDetalleProductos(),
            };

            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listProductosSeleccionados));

            HttpContext.Session.SetString("RowCounter", (numeroContador + 1).ToString());

            return PartialView("_TableProductosSeleccionadosPedidoCliente", data);
        }

        public IActionResult SeleccionarArticuloFavorito(string cantidadArticulo, string jsonArticulo)
        {
            var token = HttpContext.Session.GetString("token");

            Result res = JsonConvert.DeserializeObject<Result>(jsonArticulo);

            List<ProductosNuevoPedido> listProductosSeleccionados = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            var numeroContador = JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("RowCounter"));

            var respCargaCabeceraPedido = _clienteService.getCargaCabeceraPedido(token, "0000090208");

            listProductosSeleccionados.Add(new ProductosNuevoPedido
            {
                numeroRegistro = numeroContador.ToString(),
                codigo = res.codigoSAPArticulo,
                descripcion = res.nombre,
                listadoTipoEntregas = respCargaCabeceraPedido.listadoTipoEntrega,
                unidad = res.unidad,
                peso = res.peso.ToString(),
                descFac = res.descFactura.ToString(),
                descNc = res.descNC.ToString(),
                idl = "",
                subtotal = "",
                cantidad = cantidadArticulo,
            });
            var data = new
            {
                listadoProductosNuevoPedido = listProductosSeleccionados,

                resumenDetalleProductos = new ResumenDetalleProductos(),
            };
            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listProductosSeleccionados));

            HttpContext.Session.SetString("RowCounter", (numeroContador + 1).ToString());

            return PartialView("_TableProductosSeleccionadosPedidoCliente", data);
        }

        public IActionResult BuscarProductoCodigoSap(string codigoSapCliente, string codigoArticulo, string cantidadArticulo)
        {
            var token = HttpContext.Session.GetString("token");

            var resp = _clienteService.getProductoCodigoSap(token, codigoArticulo, codigoSapCliente);

            List<ProductosNuevoPedido> listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            var numeroContador = JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("RowCounter"));

            var respCargaCabeceraPedido = _clienteService.getCargaCabeceraPedido(token, "0000090208");

            listadoProductosNuevoPedido.Add(new ProductosNuevoPedido
            {
                numeroRegistro = numeroContador.ToString(),
                codigo = resp.result.codigoSAPArticulo,
                descripcion = resp.result.nombre,
                listadoTipoEntregas = respCargaCabeceraPedido.listadoTipoEntrega,
                unidad = resp.result.unidad,
                peso = resp.result.peso.ToString(),
                descFac = resp.result.descFactura.ToString(),
                descNc = resp.result.descNC.ToString(),
                idl = "",
                subtotal = "",
                cantidad = cantidadArticulo
            });

            var data = new
            {
                listadoProductosNuevoPedido,

                resumenDetalleProductos = new ResumenDetalleProductos(),
            };

            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listadoProductosNuevoPedido));

            HttpContext.Session.SetString("RowCounter", (numeroContador + 1).ToString());

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
                    Bodega = producto.listadoTipoEntregas.Find(te => te.porDefecto is "S").codigoTipoEntrega,
                    DescFactura = Convert.ToDouble(producto.descFac),
                    DescNotaCredito = Convert.ToDouble(producto.descNc),
                    AplicaFamilia = producto.aFamilia,
                    AplicaFinMes = producto.aFinMes
                })

            });

            var respCrearNuevoPedido = _clienteService.postProcesoFlujoAprobacion(token, parametrosPeticion);

            var listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            var resumenDetalleProductos = JsonConvert.DeserializeObject<ResumenDetalleProductos>(HttpContext.Session.GetString("SummaryProducts")) ?? new ResumenDetalleProductos();

            var mensajeProceso = respCrearNuevoPedido.mensajeRespuestaCalculoFlujo;

            ViewBag.MensajeProceso = mensajeProceso;

            var data = new
            {
                listadoProductosNuevoPedido,

                resumenDetalleProductos,
            };

            return PartialView("_TableProductosSeleccionadosPedidoCliente", data);
        }

        #endregion

        public IActionResult CargarInformacionModalInformacionCrediticia(string codigoSAP)
        {
            var token = HttpContext.Session.GetString("token");

            var datosCliente = new InicioCliente();

            var respinformacionCrediticia = _clienteService.getInformacionCrediticia(token, codigoSAP);

            datosCliente.informacionCrediticia = respinformacionCrediticia;

            return PartialView("_ModalInformacionCrediticiaCliente", datosCliente);
        }

        public IActionResult CargarInformacionModalPedidoAprobado(string numeroPedido)
        {
            var token = HttpContext.Session.GetString("token");

            var inicioCliente = new InicioCliente();

            var respPedidoAprobado = _clienteService.getPedidoAprobado(token, numeroPedido);

            inicioCliente.pedidoAprobado = respPedidoAprobado;

            return PartialView("_ModalPedidoAprobadoCliente", inicioCliente);
        }

        public IActionResult PedidosCliente()
        {
            var token = HttpContext.Session.GetString("token");

            var listaImagenes = JsonConvert.DeserializeObject<List<ListadoImagenes>>(HttpContext.Session.GetString("ListImages"));

            var listadoPedidosBPH = _clienteService.getListadoPedidosBPH(token, "borrador", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"), "");

            listadoPedidosBPH.listadoImagenes = listaImagenes;

            return View(listadoPedidosBPH);
        }

        public IActionResult FillPedidosBorrador()
        {
            var token = HttpContext.Session.GetString("token");

            var listadoPedidosBPH = _clienteService.getListadoPedidosBPH(token, "borrador", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"), "");

            return PartialView("_TablePedidosBorradorCliente", listadoPedidosBPH);
        }

        public IActionResult FillPedidosActivos()
        {
            var token = HttpContext.Session.GetString("token");

            var listadoPedidosBPH = _clienteService.getListadoPedidosBPH(token, "pendiente", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"), "");

            return PartialView("_TablePedidosActivosCliente", listadoPedidosBPH);
        }

        public IActionResult FillPedidosHistoricos()
        {
            var token = HttpContext.Session.GetString("token");

            var listadoPedidosBPH = _clienteService.getListadoPedidosBPH(token, "", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"), "");

            return PartialView("_TablePedidosHistoricosCliente", listadoPedidosBPH);
        }

        public IActionResult FillPedidosBorradorData(DateTime fechaInicio, DateTime fechaFin, string cadena = "")
        {
            var token = HttpContext.Session.GetString("token");

            var listadoPedidosBPH = _clienteService.getListadoPedidosBPH(token, "borrador", fechaInicio, fechaFin, cadena);

            if (listadoPedidosBPH.result == null)
            {
                listadoPedidosBPH.result = new List<InfoPedidoBPH>();
            }
            return PartialView("_TablePedidosBorradorCliente", listadoPedidosBPH);
        }

        public IActionResult FillPedidosActivosData(DateTime fechaInicio, DateTime fechaFin, string cadena = "")
        {
            var token = HttpContext.Session.GetString("token");

            var listadoPedidosBPH = _clienteService.getListadoPedidosBPH(token, "pendiente", fechaInicio, fechaFin, cadena);

            if (listadoPedidosBPH.result == null)
            {
                listadoPedidosBPH.result = new List<InfoPedidoBPH>();
            }
            return PartialView("_TablePedidosActivosCliente", listadoPedidosBPH);
        }

        public IActionResult FillPedidosHistoricosData(DateTime fechaInicio, DateTime fechaFin, string cadena = "", string tipoPedido = "")
        {
            var token = HttpContext.Session.GetString("token");

            var listadoPedidosBPH = _clienteService.getListadoPedidosBPH(token, tipoPedido, fechaInicio, fechaFin, cadena);

            if (listadoPedidosBPH.result == null)
            {
                listadoPedidosBPH.result = new List<InfoPedidoBPH>();
            }
            return PartialView("_TablePedidosHistoricosCliente", listadoPedidosBPH);
        }

        public IActionResult CargarInformacionModalDetallePedidoAprobado(string numeroOrden)
        {
            var token = HttpContext.Session.GetString("token");

            var listadoPedidosBPH = new ListadoPedidosBPH();

            var respDetallePedido = _clienteService.getDetallePedido(token, numeroOrden);

            listadoPedidosBPH.detallePedidoInfo = respDetallePedido;

            return PartialView("_ModalDetallePedidoAprobadoCliente", listadoPedidosBPH);
        }

        public IActionResult CargarInformacionModalDetallePedidoEntregado(string numeroOrden)
        {
            var token = HttpContext.Session.GetString("token");

            var listadoPedidosBPH = new ListadoPedidosBPH();

            var respDetallePedido = _clienteService.getDetallePedido(token, numeroOrden);

            listadoPedidosBPH.detallePedidoInfo = respDetallePedido;

            return PartialView("_ModalDetallePedidoEntregadoCliente", listadoPedidosBPH);
        }

        public IActionResult EstadoCuentaCliente(string codigoSap)
        {
            var token = HttpContext.Session.GetString("token");

            var respEstadoCuenta = _clienteService.getInfoEstadoCuenta(token, codigoSap);

            return View(respEstadoCuenta);
        }

        public IActionResult InicioReclamo()
        {
            var token = HttpContext.Session.GetString("token");
            var listaImagenes = JsonConvert.DeserializeObject<List<ListadoImagenes>>(HttpContext.Session.GetString("ListImages"));
            var respInicioReclamo = _clienteService.getReclamosCliente(token);
            respInicioReclamo.listadoImagenes = listaImagenes;
            return View(respInicioReclamo);
        }

        public IActionResult NuevoReclamo()
        {
            var token = HttpContext.Session.GetString("token");
            var listaImagenes = JsonConvert.DeserializeObject<List<ListadoImagenes>>(HttpContext.Session.GetString("ListImages"));
            var respInicioCliente = _clienteService.getInfoNuevoReclamo(token);
            respInicioCliente.listadoImagenes = listaImagenes;
            return View(respInicioCliente);
        }


        //public IActionResult GuardarReclamo(string motivo, string factura, string producto, string referencia, string asunto, string observaciones, IFormFile fotografiaMaterial, IFormFile copiaFactura)
        public IActionResult GuardarReclamo(string motivo, string factura, string producto, string referencia, string asunto, string observaciones, string fotografiaMaterial, string copiaFactura)
        {
            var token = HttpContext.Session.GetString("token");
            /*******************************/
            /*byte[] fotografiaMaterialBytes = null;
            byte[] copiaFacturaBytes = null;

            if (fotografiaMaterial != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    fotografiaMaterial.CopyTo(memoryStream);
                    fotografiaMaterialBytes = memoryStream.ToArray();
                }
            }

            if (copiaFactura != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    copiaFactura.CopyTo(memoryStream);
                    copiaFacturaBytes = memoryStream.ToArray();
                }
            }*/
            /*********************************/
            //var respInicioCliente = _clienteService.postNuevoReclamo(token, motivo, factura, producto, referencia, asunto, observaciones, fotografiaMaterialBytes, copiaFacturaBytes);
            var respInicioCliente = _clienteService.postNuevoReclamo(token, motivo, factura, producto, referencia, asunto, observaciones, fotografiaMaterial, copiaFactura);
            return View(respInicioCliente);
        }

        public IActionResult ConfirmacionCliente(int codigoReclamo, int calificacionServicio, string observaciones)
        {
            var token = HttpContext.Session.GetString("token");
            var respConfirmacionCliente = _clienteService.postConfirmacionCliente(token, codigoReclamo, calificacionServicio, observaciones);
            return View(respConfirmacionCliente);
        }

        public IActionResult ReclamoEnProgreso(int codigoReclamo)
        {
            var token = HttpContext.Session.GetString("token");
            var listaImagenes = JsonConvert.DeserializeObject<List<ListadoImagenes>>(HttpContext.Session.GetString("ListImages"));
            var respDetalleReclamoCliente = _clienteService.getDetalleReclamoCliente(token, codigoReclamo);

            respDetalleReclamoCliente.listadoImagenes = listaImagenes;
            return View(respDetalleReclamoCliente);
        }
    }
}
