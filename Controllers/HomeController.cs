using Microsoft.AspNetCore.Mvc;
using Portal_MovilEsales.Models;
using Portal_MovilEsales.Services;
using System.Diagnostics;

namespace Portal_MovilEsales.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService _service;

        public HomeController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CambioPasswordMenu()
        {
            return View(); 
        }

        public IActionResult ChangePassword(string oldPass, string newPass)
        {
            var token = HttpContext.Session.GetString("token");
            Authentication auth = new Authentication
            {
                ClaveAnterior = oldPass,
                NuevaClave = newPass,
                navegadorweb = "Edge"
            };
            string res = _service.changePassword(token, auth);
            return Json(res);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}