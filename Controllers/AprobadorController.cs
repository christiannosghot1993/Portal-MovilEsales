using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_MovilEsales.Services.AprobadorServices;
using Portal_MovilEsales.Services.AprobadorServices.ViewModels.InicioAprobador;
using Portal_MovilEsales.Services.AprobadorServices.ViewModels.PedidosReporte;

namespace Portal_MovilEsales.Controllers
{
    [Authorize]
    public class AprobadorController : Controller
    {
        private readonly IAprobadorService _aprobadorService;

        public AprobadorController(IAprobadorService aprobadorService)
        {
            _aprobadorService = aprobadorService;
        }

        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("token");

            var respInicioAprobador = _aprobadorService.getInfoInicioAprobador(token);

            HttpContext.Session.SetString("contactoWhatsApp", "https://wa.me/" + respInicioAprobador.contactoWhatsApp);

            return View(respInicioAprobador);
        }

        public IActionResult CargarInformacionModalDetallePedidoPendiente(string numeroOrden)
        {
            var token = HttpContext.Session.GetString("token");

            var inicioAprobador = new InicioAprobador();

            var respDetallePedido = _aprobadorService.getDetallePedidoAprobador(token, numeroOrden);

            inicioAprobador.detallePedidoPendiente = respDetallePedido;

            return PartialView("_ModalDetallePedidoPendiente", inicioAprobador);
        }


        public IActionResult CargarInformacionModalDetallePedidoReporte(string numeroOrden)
        {
            var token = HttpContext.Session.GetString("token");

            var inicioAprobador = new InicioAprobador();

            var respDetallePedido = _aprobadorService.getDetallePedidoAprobador(token, numeroOrden);

            inicioAprobador.detallePedidoPendiente = respDetallePedido;

            return PartialView("_ModalDetallePedidoReporte", inicioAprobador);
        }

        public IActionResult ProcesarPedido(string numeroOrden, string aprobacionDescuento, string aprobacionCondicionesEspeciales,
                                           string aprobacionNotaCredito, string aprobacionGeneral, string mensaje)
        {
            var token = HttpContext.Session.GetString("token");

            var respProcesaPedido = _aprobadorService.postProcesaPedido(token, numeroOrden, aprobacionDescuento, aprobacionCondicionesEspeciales,
                                                                        aprobacionNotaCredito, aprobacionGeneral, mensaje);

            return View();
        }

        public IActionResult FillPedidosPorFitroData(string fechaInicio, string fechaFin, string orden, string cliente, string asesor, string estado)
        {
            var token = HttpContext.Session.GetString("token");

            var reportePedido = new ReportePedido();

            var listadoPedidosReporte = _aprobadorService.getListadoPedidosReporte(token, fechaInicio, fechaFin, orden, cliente, asesor, estado);
            
            listadoPedidosReporte.result ??= new List<InfoPedidoReporte>();

            reportePedido.listadoPedidosReporte = listadoPedidosReporte;

            return PartialView("_TablePedidosReporte", reportePedido);
        }

        public IActionResult ReportePedidos()
        {
            var reportePedidos = new ReportePedido();

            return View(reportePedidos);
        }
    }
}
