using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portal_MovilEsales.Controllers
{
    //[Authorize]
    public class AsesorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PedidoCatalogoProductos()
        {
            return View();
        }

        public IActionResult PedidoProductosSeleccionados()
        {
            return View();
        }
        public IActionResult EstadoCuentaAsesor()
        {
            return View();
        }

        public IActionResult Clientes()
        {
            return View();
        }

        public IActionResult InformacionCliente()
        {
            return View();
        }

        public IActionResult Pedidos()
        {
            return View();
        }
    }
}
