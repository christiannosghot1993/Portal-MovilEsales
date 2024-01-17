using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portal_MovilEsales.Controllers
{
    [Authorize]
    public class AsesorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MisClientes()
        {
            return View();
        }

        public IActionResult Pedidos()
        {
            return View();
        }
    }
}
