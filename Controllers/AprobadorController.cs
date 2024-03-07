using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_MovilEsales.Services.AprobadorServices;
using Portal_MovilEsales.Services.AprobadorServices.ViewModels.InicioAprobador;

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


        public IActionResult ReportePedidos()
        {
            return View();
        }
    }
}
