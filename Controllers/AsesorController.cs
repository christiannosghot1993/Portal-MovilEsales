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
        public IActionResult EstadoCuentaAsesor()
        {
            return View();
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
            var respPedidoAprobado = _asesorService.getPedidoAprobado(token, numeroPedido);
            dc.pedidoAprobado = respPedidoAprobado;
            return PartialView("_ModalPedidoAprobado", dc);
        }

        public IActionResult Pedidos()
        {
            var token = HttpContext.Session.GetString("token");
            var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, "borrador", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"));
            return View(listadoPedidosBPH);
        }

        public IActionResult FillPedidosActivos()
        {
            var token = HttpContext.Session.GetString("token");
            var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, "pendiente", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"));
            return PartialView("_TablePedidosActivos", listadoPedidosBPH);
        }
        public IActionResult FillPedidosBorrador()
        {
            var token = HttpContext.Session.GetString("token");
            var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, "borrador", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"));
            return PartialView("_TablePedidosBorrador", listadoPedidosBPH);
        }

        public IActionResult FillPedidosHistoricos()
        {
            var token = HttpContext.Session.GetString("token");
            var listadoPedidosBPH = _asesorService.getListadoPedidosBPH(token, "", DateTime.Parse("2020-01-31"), DateTime.Parse("2024-02-28"));
            return PartialView("_TablePedidosHistoricos", listadoPedidosBPH);
        }
    }
}
