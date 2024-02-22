using Microsoft.AspNetCore.Mvc;

namespace Portal_MovilEsales.Controllers
{
    public class AprobadorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReportePedidos()
        {
            return View();
        }
    }
}
