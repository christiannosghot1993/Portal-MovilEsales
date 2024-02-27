using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_MovilEsales.Services.AsesorServices;
using Portal_MovilEsales.Services.AsesorServices.ViewModels;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DatosCliente;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.PedidoAprobado;

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
            var token=HttpContext.Session.GetString("token");
            var respInicioAsesor = _asesorService.getInfoInicioAsesor(token);
            var respDatosCliente = _asesorService.getDatosCliente(token, "0000090208");
            HttpContext.Session.SetString("contactoWhatsApp", "https://wa.me/" + respInicioAsesor.contactoWhatsApp);
            return View(respInicioAsesor);
        }
        public IActionResult PedidoCatalogoProductos()
        {
            return View();
        }

        public IActionResult PoliticaComercial()
        {
            return View();
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
            DatosCliente dc=new DatosCliente();
            var respPedidoAprobado = _asesorService.getPedidoAprobado(token, "10");
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

            //var datosCliente = new DetallePedidoInfo();

            var respDetallePedido = _asesorService.getDetallePedido(token, numeroOrden);

            //datosCliente.detallePedidoInfo = respDetallePedido;

            return PartialView("_ModalDetallePedidoEntregado", datosCliente);
        }

        public IActionResult CargarInformacionModalDetallePedidoAprobado(string numeroOrden)
        {
            var token = HttpContext.Session.GetString("token");

            //var datosCliente = new DatosCliente();

            var respDetallePedido = _asesorService.getDetallePedido(token, numeroOrden);

            //datosCliente.detallePedidoInfo = respDetallePedido;

            return PartialView("_ModalDetallePedidoAprobado", datosCliente);
        }

        public IActionResult Pedidos()
        {
            return View();
        }
    }
}
