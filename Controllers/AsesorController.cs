using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal_MovilEsales.Services.AsesorServices;
using Portal_MovilEsales.Services.AsesorServices.ViewModels;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DatosCliente;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.EstadoCuenta;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.ProductoExcel;

namespace Portal_MovilEsales.Controllers
{
    [Authorize]
    public class AsesorController : Controller
    {
        private readonly IAsesorService _asesorService;

        public AsesorController(IAsesorService asesorService)
        {
            _asesorService = asesorService;
        }

        public IActionResult Index(string cadena)
        {
            var token = HttpContext.Session.GetString("token");

            var respInicioAsesor = _asesorService.getInfoInicioAsesor(token, cadena);

            //var respDatosCliente = _asesorService.getDatosCliente(token, "0000090208");

            HttpContext.Session.SetString("contactoWhatsApp", "https://wa.me/" + respInicioAsesor.contactoWhatsApp);

            return View(respInicioAsesor);
        }

        public IActionResult PedidoCatalogoProductos(string? codigoSAPCliente = null)
        {
            var listaProductosSeleccionados = new List<ProductosNuevoPedido>();

            var resumenDetalleProductos = new ResumenDetalleProductos();

            var contadorRegistros = 1;

            var isCheckedAllProducts = false;

            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listaProductosSeleccionados));

            HttpContext.Session.SetString("SummaryProducts", JsonConvert.SerializeObject(resumenDetalleProductos));

            HttpContext.Session.SetString("RowCounter", contadorRegistros.ToString());

            var token = HttpContext.Session.GetString("token");

            var nuevoPedido = new NuevoPedido();

            if (codigoSAPCliente is not null)
            {
                nuevoPedido.cargaCabeceraPedido = _asesorService.getCargaCabeceraPedido(token, codigoSAPCliente);
            }

            var respListaFamiliasProductos = _asesorService.getFamiliaProductos(token);

            var respListaProductosFavoritos = codigoSAPCliente is null ? new ListadoProductosFavoritos() :
                                              _asesorService.getProductosFavoritos(token, codigoSAPCliente);

            nuevoPedido.listaFamiliaProductos = respListaFamiliasProductos;

            nuevoPedido.listadProductosFavoritos = respListaProductosFavoritos;

            return View(nuevoPedido);
        }


        public IActionResult BuscarPedido(string numeroOrden, string codigoSAPCliente)
        {
            var listaProductosSeleccionados = new List<ProductosNuevoPedido>();

            var resumenDetalleProductos = new ResumenDetalleProductos();

            var contadorRegistros = 1;



            HttpContext.Session.SetString("SummaryProducts", JsonConvert.SerializeObject(resumenDetalleProductos));

            HttpContext.Session.SetString("RowCounter", contadorRegistros.ToString());

            var token = HttpContext.Session.GetString("token");

            var respDetallePedido = _asesorService.getDetallePedido(token, numeroOrden);


            var nuevoPedido = new NuevoPedido();
            nuevoPedido.cargaCabeceraPedido = _asesorService.getCargaCabeceraPedido(token, codigoSAPCliente);
            List<ProductosNuevoPedido> productosSeleccionados = new List<ProductosNuevoPedido>();
            foreach (var item in respDetallePedido.detallePedido)
            {
                ProductosNuevoPedido prod = new ProductosNuevoPedido
                {
                    numeroRegistro = "",
                    bloqueado = false,
                    codigo = item.codigoSAP,
                    descripcion = item.nombreProducto,
                    //listadoTipoEntregas = nuevoPedido.cargaCabeceraPedido.listadoTipoEntrega,
                    listadoBodegas = nuevoPedido.cargaCabeceraPedido.listadoBodegas,
                    unidad = item.unidad,
                    peso = item.peso,
                    descFac = item.descFactura,
                    descNc = item.descNotaCredito,
                    idl = "",
                    subtotal = item.subtotal,
                    cantidad = item.cantidad,
                    aFinMes = false,
                    aFamilia = false,
                };
                productosSeleccionados.Add(prod);
            }
            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(productosSeleccionados));
            nuevoPedido.listadoProductosNuevoPedido = productosSeleccionados;
            nuevoPedido.resumenDetalleProductos = new ResumenDetalleProductos
            {
                importeBruto = respDetallePedido.importeBruto,
                descuentoBase = respDetallePedido.descuentoBase,
                subTotal1 = respDetallePedido.subTotal1,
                descuentoPago = respDetallePedido.descuentoPago,
                descuentoRetiro = respDetallePedido.descuentoRetiro,
                descuentoVarios = respDetallePedido.descuentoVarios,
                descuentoPeso = respDetallePedido.descuentoPeso,
                subTotal2 = respDetallePedido.subTotal2,
                iva = respDetallePedido.iva,
                seguroTransporte = respDetallePedido.seguroTransporte,
                valorTotal = respDetallePedido.valorTotal,
                valorNcsinIva = respDetallePedido.valorNcsinIva,
                margenPor = respDetallePedido.margenPor,
            };
            nuevoPedido.informacionEntrega = new InformacionEntrega
            {
                fechaEntrega = respDetallePedido.fechaEntrega,
                numeroOrden = respDetallePedido.numeroOrden,
                direccionEntrega = respDetallePedido.direccionEntrega,
                observacion = respDetallePedido.observacion,
                contacto = respDetallePedido.contacto,
                soporteVenta = "",
            };

            var respListaFamiliasProductos = _asesorService.getFamiliaProductos(token);

            var respListaProductosFavoritos = codigoSAPCliente is null ? new ListadoProductosFavoritos() :
                                              _asesorService.getProductosFavoritos(token, codigoSAPCliente);

            nuevoPedido.listaFamiliaProductos = respListaFamiliasProductos;

            nuevoPedido.listadProductosFavoritos = respListaProductosFavoritos;
            return View("PedidoCatalogoProductos", nuevoPedido);
        }

        public IActionResult FillCabeceraNuevoPedido(string codigoSAPCliente)
        {
            var token = HttpContext.Session.GetString("token");

            var nuevoPedido = new NuevoPedido();

            var respCargaCabeceraPedido = _asesorService.getCargaCabeceraPedido(token, codigoSAPCliente);

            nuevoPedido.cargaCabeceraPedido = respCargaCabeceraPedido;

            return PartialView("_FormCabeceraNuevoPedido", nuevoPedido);
        }

        public IActionResult GetProductosPorFamilia(string familia, string codigoSAPCliente)
        {
            var token = HttpContext.Session.GetString("token");

            var nuevoPedido = new NuevoPedido();

            var respProductosPorFamilia = _asesorService.getProductosPorFamilia(token, familia, codigoSAPCliente);

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
                    //Bodega = "QU00",
                    //Bodega = producto.BodegaProductoSel,
                    Bodega = producto.BodegaProductoSel == null ? producto.listadoBodegas.Find(bo => bo.porDefecto is "S").codigoBodega : producto.BodegaProductoSel,
                    DescFactura = Convert.ToDouble(producto.descFac),
                    DescNotaCredito = Convert.ToDouble(producto.descNc)
                })

            });

            var nuevoPedido = new NuevoPedido();

            var productosNuevoPedido = new List<ProductosNuevoPedido>();

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

                    //nuevos campos SB marzo 2024
                    precio = producto.precio,
                    descBase = producto.descBase,
                    precioFinal = producto.precioFinal,
                    subtotal2 = producto.subtotal2,

                    //listadoTipoEntregas = respCargaCabeceraPedido.listadoTipoEntrega
                    listadoBodegas = respCargaCabeceraPedido.listadoBodegas,
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

            nuevoPedido.mensajeProceso = "test";

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
                    //Bodega = producto.listadoTipoEntregas.Find(te => te.porDefecto is "S").codigoTipoEntrega,
                    Bodega = producto.listadoBodegas.Find(bo => bo.porDefecto is "S").codigoBodega,
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

        [HttpPost]
        [Route("File/Upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se ha seleccionado ningún archivo.");
            }

            // Crear el directorio si no existe
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/archivos")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/archivos"));
            }

            // Generar una ruta completa para guardar el archivo
            var filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/archivos"), file.FileName);

            // Guardar el archivo en el servidor
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { message = filePath });
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
                string? contactoEntrega = null,
                string? urlArchivo=null)
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
                ArchivoSoporte = urlArchivo,
                detallePedido = listaProductos.Select((producto) => new
                {
                    CodigoSAPArticulo = producto.codigo,
                    Cantidad = producto.cantidad,
                    Unidad = producto.unidad,
                    //Bodega = producto.listadoTipoEntregas.Find(te => te.porDefecto is "S").codigoTipoEntrega,
                    Bodega = producto.listadoBodegas.Find(bo => bo.porDefecto is "S").codigoBodega,
                    DescFactura = Convert.ToDouble(producto.descFac),
                    DescNotaCredito = Convert.ToDouble(producto.descNc),
                    AplicaFamilia = producto.aFamilia,
                    AplicaFinMes = producto.aFinMes
                })

            });

            var respCrearNuevoPedido = _asesorService.postProcesoFlujoAprobacion(token, parametrosPeticion);

            var listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            var resumenDetalleProductos = JsonConvert.DeserializeObject<ResumenDetalleProductos>(HttpContext.Session.GetString("SummaryProducts")) ?? new ResumenDetalleProductos();

            var mensajeProceso = respCrearNuevoPedido.mensajeRespuestaCalculoFlujo;

            ViewBag.MensajeProceso = mensajeProceso;

            var data = new
            {
                listadoProductosNuevoPedido,

                resumenDetalleProductos,
            };

            //return PartialView("_TableProductosSeleccionadosPedido", data);
            return Json(mensajeProceso);
        }

        public IActionResult PoliticaComercial()
        {
            return View();
        }

        public IActionResult ActualizarTablaProductosSeleccionados(string codigoSAPCliente, string codigoSAPArticulo, string numeroRegistro, string entrega, string descFactura, string descNc, string cantidad, string aFinMes, string aFamilia)
        {
            var token = HttpContext.Session.GetString("token");

            var listProductosSeleccionados = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            var productoPorActualizar = listProductosSeleccionados.FirstOrDefault(p => p.codigo == codigoSAPArticulo && p.numeroRegistro == numeroRegistro);

            var respCargaCabeceraPedido = _asesorService.getCargaCabeceraPedido(token, codigoSAPCliente);

            var indexProductoPorActualizar = listProductosSeleccionados.IndexOf(productoPorActualizar);

            //var listaTipoEntregas = respCargaCabeceraPedido.listadoTipoEntrega.Select((te) => new ListadoTipoEntrega
            //{
            //    codigoTipoEntrega = te.codigoTipoEntrega,
            //    descripcionTipoEntrega = te.descripcionTipoEntrega,
            //    porDefecto = te.codigoTipoEntrega == entrega ? "S" : "N"
            //});

            var listaBodegas= respCargaCabeceraPedido.listadoBodegas.Select((bo) => new ListadoBodegas
            {
                codigoBodega = bo.codigoBodega,
                descripcionBodega = bo.descripcionBodega,
                porDefecto = bo.codigoBodega == entrega ? "S" : "N"
            });

            productoPorActualizar.BodegaProductoSel = entrega;

            //productoPorActualizar.listadoTipoEntregas = listaTipoEntregas.ToList();
            productoPorActualizar.listadoBodegas = listaBodegas.ToList();

            productoPorActualizar.descFac = descFactura;

            productoPorActualizar.descNc = descNc;

            productoPorActualizar.cantidad = cantidad;

            productoPorActualizar.aFinMes = aFinMes is "S";

            productoPorActualizar.aFamilia = aFamilia is "S";

            listProductosSeleccionados[indexProductoPorActualizar] = productoPorActualizar;

            var data = new
            {
                listadoProductosNuevoPedido = listProductosSeleccionados,

                resumenDetalleProductos = new ResumenDetalleProductos(),
            };

            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listProductosSeleccionados));

            return PartialView("_TableProductosSeleccionadosPedido", data);

        }

        public IActionResult SeleccionarArticuloPorFamilia(string cantidadArticulo, string codigoSAPCliente, string codigoSAPArticulo, string familia)
        {
            var token = HttpContext.Session.GetString("token");

            var listProductosSeleccionados = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            var numeroContador = JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("RowCounter"));

            var listadoProductosPorFamilia = _asesorService.getProductosPorFamilia(token, familia, codigoSAPCliente);

            var productoPorAgregar = listadoProductosPorFamilia.result.FirstOrDefault(p => p.codigoSAPArticulo == codigoSAPArticulo);

            var respCargaCabeceraPedido = _asesorService.getCargaCabeceraPedido(token, codigoSAPCliente);

            listProductosSeleccionados.Add(new ProductosNuevoPedido
            {
                numeroRegistro = numeroContador.ToString(),
                codigo = productoPorAgregar.codigoSAPArticulo,
                descripcion = productoPorAgregar.nombre,
                //listadoTipoEntregas = respCargaCabeceraPedido.listadoTipoEntrega,
                listadoBodegas = respCargaCabeceraPedido.listadoBodegas,
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

            return PartialView("_TableProductosSeleccionadosPedido", data);
        }

        public IActionResult 
            SeleccionarArticuloFavorito(string codigoSAPCliente, string cantidadArticulo, string jsonArticulo)
        {
            var token = HttpContext.Session.GetString("token");

            Result res = JsonConvert.DeserializeObject<Result>(jsonArticulo);

            List<ProductosNuevoPedido> listProductosSeleccionados = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            var numeroContador = JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("RowCounter"));
             
            var respCargaCabeceraPedido = _asesorService.getCargaCabeceraPedido(token, codigoSAPCliente);

            listProductosSeleccionados.Add(new ProductosNuevoPedido
            {
                numeroRegistro = numeroContador.ToString(),
                codigo = res.codigoSAPArticulo,
                descripcion = res.nombre,
                //listadoTipoEntregas = respCargaCabeceraPedido.listadoTipoEntrega,
                listadoBodegas = respCargaCabeceraPedido.listadoBodegas,
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

            return PartialView("_TableProductosSeleccionadosPedido", data);
        }

        public IActionResult BuscarProductoCodigoSap(string codigoSapCliente, string codigoArticulo, string cantidadArticulo)
        {
            var token = HttpContext.Session.GetString("token");
            var resp = _asesorService.getProductoCodigoSap(token, codigoArticulo, codigoSapCliente);
            List<ProductosNuevoPedido> listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));
            var numeroContador = JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("RowCounter"));
            var respCargaCabeceraPedido = _asesorService.getCargaCabeceraPedido(token, codigoSapCliente);
            listadoProductosNuevoPedido.Add(new ProductosNuevoPedido
            {
                numeroRegistro = numeroContador.ToString(),
                codigo = resp.result.codigoSAPArticulo,
                descripcion = resp.result.nombre,
                //listadoTipoEntregas = respCargaCabeceraPedido.listadoTipoEntrega,
                listadoBodegas = respCargaCabeceraPedido.listadoBodegas,
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
            HttpContext.Session.SetString("ListadoEstadoCuenta", JsonConvert.SerializeObject(respEstadoCuenta.detalleEstadoCuenta));
            return View(respEstadoCuenta);
        }

        public IActionResult Clientes(string cadena)
        {
            var token = HttpContext.Session.GetString("token");
            var respInicioAsesor = _asesorService.getInfoInicioAsesor(token, cadena);
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

        public IActionResult DescargarEstadoCuenta()
        {
            var listadoEstadoEcuenta = HttpContext.Session.GetString("ListadoEstadoCuenta");
            // Deserializar el JSON en una lista de objetos o DataTable
            List<ListadoEstadoCuenta> data = JsonConvert.DeserializeObject<List<ListadoEstadoCuenta>>(listadoEstadoEcuenta);

            // Crear un nuevo libro de Excel
            using (var workbook = new XLWorkbook())
            {
                // Agregar una hoja al libro
                var worksheet = workbook.Worksheets.Add("EstadoCuenta");

                // Agregar encabezados
                var headers = worksheet.Row(1);
                headers.Cell(1).Value = "FACTURA";
                headers.Cell(2).Value = "FECHA";
                headers.Cell(3).Value = "VALOR";
                headers.Cell(4).Value = "TERMINO PAGO";
                headers.Cell(5).Value = "DOCUMENTOS SAP";
                headers.Cell(6).Value = "RETENCIONES";
                headers.Cell(7).Value = "ABONOS";
                headers.Cell(8).Value = "NOTAS DE CREDITO";
                headers.Cell(9).Value = "FECHA VENCIMIENTO";
                headers.Cell(10).Value = "SALDO POR COBRAR";
                headers.Cell(11).Value = "DIAS VENCIDOS";
                // Agregar más encabezados según sea necesario

                // Escribir los datos debajo de los encabezados
                worksheet.Cell(2, 1).InsertData(data);

                // Guardar el libro en un MemoryStream
                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    // Devolver el archivo de Excel como una descarga al cliente
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EstadoCuenta.xlsx");
                }
            }
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

        [HttpPost]
        public IActionResult SubirExcelProductos(IFormFile file)
        {
            var token = HttpContext.Session.GetString("token");
            List<ProductosNuevoPedido> listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));
            if (file != null && file.Length > 0)
            {
                // Guardar el archivo en una ubicación temporal
                var filePath = Path.GetTempPath();
                var filePathAndName = filePath + file.FileName;
                using (var stream = new FileStream(filePathAndName, FileMode.Create))
                {
                    file.CopyTo(stream);
                }


                // Procesar el archivo Excel utilizando ClosedXML
                using (var workbook = new XLWorkbook(filePathAndName))
                {
                    // Obtener la primera hoja del libro de trabajo
                    var worksheet = workbook.Worksheet(1);

                    // Iterar sobre las filas del archivo Excel
                    List<ProductoExcelModelRequest> listProd = new List<ProductoExcelModelRequest>();
                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        // Obtener los valores de las celdas en cada fila
                        var codigoSAP = row.Cell(1).Value.ToString();
                        var cantidad = row.Cell(2).Value.ToString();
                        var descFactura = row.Cell(3).Value.ToString();
                        var descNC = row.Cell(4).Value.ToString();
                        if (!string.IsNullOrEmpty(codigoSAP))
                        {
                            listProd.Add(new ProductoExcelModelRequest
                            {
                                CodigoSAP = codigoSAP,
                                Cantidad = !string.IsNullOrEmpty(cantidad) ? int.Parse(cantidad) : 0,
                                DescFactura = !string.IsNullOrEmpty(descFactura) ? double.Parse(descFactura) : 0,
                                DescNC = !string.IsNullOrEmpty(descNC) ? double.Parse(descNC) : 0
                            });
                        }
                    }
                    if (listProd.Any())
                    {
                        ProductoExcel resp = _asesorService.getProductosExcel(token, new ProductoExcelRequest
                        {
                            detallePedido = listProd
                        });
                        var respCargaCabeceraPedido = _asesorService.getCargaCabeceraPedido(token, "0000090208");

                        foreach (var item in resp.result)
                        {
                            var numeroContador = JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("RowCounter"));
                            listadoProductosNuevoPedido.Add(new ProductosNuevoPedido
                            {
                                numeroRegistro = numeroContador.ToString(),
                                bloqueado = false,
                                codigo = item.codigoSAPArticulo,
                                descripcion = item.nombre,
                                //listadoTipoEntregas = respCargaCabeceraPedido.listadoTipoEntrega,
                                listadoBodegas = respCargaCabeceraPedido.listadoBodegas,
                                unidad = item.unidad,
                                peso = item.peso.ToString(),
                                descFac = item.descFactura.ToString(),
                                descNc = item.descNC.ToString(),
                                idl = "0",
                                subtotal = "0",
                                cantidad = item.cantidad.ToString(),
                                aFinMes = false,
                                aFamilia = false
                            });
                            HttpContext.Session.SetString("RowCounter", (numeroContador + 1).ToString());
                        }

                    }
                }

                // Eliminar el archivo temporal después de usarlo
                System.IO.File.Delete(filePathAndName);
                var data = new
                {
                    listadoProductosNuevoPedido,
                    resumenDetalleProductos = new ResumenDetalleProductos()
                };
                HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listadoProductosNuevoPedido));
                return PartialView("_TableProductosSeleccionadosPedido", data);
            }
            else
            {

                var data = new
                {
                    listadoProductosNuevoPedido,
                    resumenDetalleProductos = new ResumenDetalleProductos()
                };
                return PartialView("_TableProductosSeleccionadosPedido", data);
            }

        }

        public IActionResult eliminarArticulo(string codigoArticulo)
        {
            List<ProductosNuevoPedido> listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));
            listadoProductosNuevoPedido = listadoProductosNuevoPedido.Where(x => x.codigo != codigoArticulo).ToList();
            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listadoProductosNuevoPedido));
            var data = new
            {
                listadoProductosNuevoPedido,
                resumenDetalleProductos = new ResumenDetalleProductos()
            };
            return PartialView("_TableProductosSeleccionadosPedido", data);
        }

        public IActionResult marcarDescuento(bool isChecked, string codigoArticulo)
        {
            List<ProductosNuevoPedido> listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));
            int indiceProducto = listadoProductosNuevoPedido.FindIndex(x => x.codigo == codigoArticulo);
            listadoProductosNuevoPedido[indiceProducto].isChecked = isChecked;
            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listadoProductosNuevoPedido));
            return Json(listadoProductosNuevoPedido);
        }

        public IActionResult MarcarAllProductos(bool isChecked)
        {
            List<ProductosNuevoPedido> listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));

            listadoProductosNuevoPedido.ForEach(producto => producto.isChecked = isChecked);

            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listadoProductosNuevoPedido));

            var data = new
            {
                listadoProductosNuevoPedido,

                resumenDetalleProductos = new ResumenDetalleProductos(),
            };

            return PartialView("_TableProductosSeleccionadosPedido", data);
        }

        public IActionResult establecerDescuento(string tipoDescuento, string valor)
        {
            List<ProductosNuevoPedido> listadoProductosNuevoPedido = JsonConvert.DeserializeObject<List<ProductosNuevoPedido>>(HttpContext.Session.GetString("SelectedProducts"));
            if (tipoDescuento.Equals("f"))
            {
                listadoProductosNuevoPedido
                .Where(p => p.isChecked)
                .ToList()
                .ForEach(p => p.descFac = valor);
            }
            else
            {
                //nc
                listadoProductosNuevoPedido
                .Where(p => p.isChecked)
                .ToList()
                .ForEach(p => p.descNc = valor);
            }

            HttpContext.Session.SetString("SelectedProducts", JsonConvert.SerializeObject(listadoProductosNuevoPedido));
            var data = new
            {
                listadoProductosNuevoPedido,
                resumenDetalleProductos = new ResumenDetalleProductos()
            };
            return PartialView("_TableProductosSeleccionadosPedido", data);
        }
    }
}
