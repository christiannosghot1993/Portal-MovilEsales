using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal_MovilEsales.Services.AdministradorServices;
using Portal_MovilEsales.Services.AdministradorServices.ViewModels;
using Portal_MovilEsales.Services.AdministradorServices.ViewModels.DetalleReclamo;
using System.Security.Policy;

namespace Portal_MovilEsales.Controllers
{
    [Authorize]
    public class AdministradorController : Controller
    {
        private readonly IAdministradorService _administradorService;

        public AdministradorController(IAdministradorService administradorService)
        {
            _administradorService = administradorService;
        }

        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("token");
            var respInicioAsesor = _administradorService.getInfoInicioAdministrador(token);
            //var respReclamoIndividual = _administradorService.getDatosCliente(token, "0000090208");
            //HttpContext.Session.SetString("contactoWhatsApp", "https://wa.me/" + respInicioAsesor.contactoWhatsApp);
            return View(respInicioAsesor);
        }

        public IActionResult Reclamo()
        {
            var token = HttpContext.Session.GetString("token");
            var respInicioAsesor = _administradorService.getInfoInicioAdministrador(token);
            //var respReclamoIndividual = _administradorService.getDatosCliente(token, "0000090208");
            //HttpContext.Session.SetString("contactoWhatsApp", "https://wa.me/" + respInicioAsesor.contactoWhatsApp);
            return View(respInicioAsesor);
        }

        public IActionResult DetalleReclamo(int codigoReclamo)
        {
            var token = HttpContext.Session.GetString("token");
            var respDetalleReclamo = _administradorService.getDetalleReclamo(token, codigoReclamo);
            return View(respDetalleReclamo);
        }

        public IActionResult DatosComplementariosReclamo(int codigoReclamo, int codigoDepartamento, int diasParaResolver, string correosANotificar, string estado, string observacion)
        {
            var token = HttpContext.Session.GetString("token");

            var respDatosComplementariosReclamo = _administradorService.postDatosComplementariosReclamo(token, codigoReclamo, codigoDepartamento, diasParaResolver, correosANotificar, estado, observacion);

            return View(respDatosComplementariosReclamo);
        }

        public IActionResult ReclamoIndividual(int codigoReclamo)
        {
            var token = HttpContext.Session.GetString("token");
            var respReclamoIndividual = _administradorService.getReclamoIndividual(token, codigoReclamo);
            return View(respReclamoIndividual);
        }

        public IActionResult Acciones(int codigoReclamo, string estado, string accion, string observaciones, string archivoBase64)
        {
            var token = HttpContext.Session.GetString("token");

            var respAccionesEnviadas = _administradorService.postAccionesRealizadas(token, codigoReclamo, estado, accion, observaciones, archivoBase64);

            return View(respAccionesEnviadas);
        }

        public IActionResult Clientes()
        {
            Console.WriteLine("Bienvenido");
            return View();
        }




    }
}
