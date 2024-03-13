using ClosedXML.Excel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal_MovilEsales.Models;
using Portal_MovilEsales.Services;
using Portal_MovilEsales.Services.AsesorServices;
using System.Security.Claims;

namespace Portal_MovilEsales.Controllers
{
    public class LoginController : Controller
    {
        private IService _service;
        public LoginController(IService service)
        {
            _service = service;
        }
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambioContrasenaEmail()
        {
            return View();
        }

        public ActionResult CambioContrasenaPass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesarFormulario([FromForm(Name = "g-recaptcha-response")] string gRecaptchaResponse, [FromForm(Name = "userInput")] string usuario, [FromForm(Name = "passwordInput")] string password)
        {
            if (string.IsNullOrEmpty(gRecaptchaResponse))
            {
                // No se proporcionó el token reCAPTCHA
                return Content("Error: No se proporcionó el token reCAPTCHA.");
            }

            string recaptchaSecretKey = "6Ld_VzApAAAAAMpEXARtyVwWW5GlgwR27b7ffmfm";
            string recaptchaUrl = "https://www.google.com/recaptcha/api/siteverify";

            using (var httpClient = new HttpClient())
            {
                var postData = new Dictionary<string, string>
            {
                { "secret", recaptchaSecretKey },
                { "response", gRecaptchaResponse }
            };

                var response = await httpClient.PostAsync(recaptchaUrl, new FormUrlEncodedContent(postData));

                var responseContent = await response.Content.ReadAsStringAsync();

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(responseContent);

                if (captchaResponse.success)
                {
                    // reCAPTCHA válido, puedes procesar el formulario aquí
                    var respIniciarSesion = _service.iniciarSesion(new IniciarSesionRequest
                    {
                        email = usuario,
                        password = password,
                    });

                    if ((bool)respIniciarSesion.success)
                    {
                        var perfil = (int)respIniciarSesion.codigoperfil switch
                        {
                            1 => "Cliente",
                            2 => "Asesor",
                            3 => "Aprobador",
                            4 => "Administrador",
                        };

                        //string perfil = "";

                        //switch ((int)respIniciarSesion.codigoperfil)
                        //{
                        //    case 1:
                        //        perfil = "Cliente";
                        //        break;
                        //    case 2:
                        //        perfil = "Asesor";
                        //        break;
                        //    case 3:
                        //        perfil = "Arobador";
                        //        break;
                        //    case 4:
                        //        perfil = "Administrador";
                        //        break;
                        //}

                        /*perfil = "Asesor";*///comentar

                        HttpContext.Session.SetString("perfil", perfil);

                        HttpContext.Session.SetString("token", (string)respIniciarSesion.result);

                        CrearClaims((string)respIniciarSesion.result, perfil, usuario);

                        switch (perfil)
                        {
                            case "Cliente":
                                return RedirectToAction("Index", "Cliente");
                            case "Asesor":
                                return RedirectToAction("Index", "Asesor");
                            case "Aprobador":
                                return RedirectToAction("Index", "Aprobador");
                            case "Administrador":
                                break;
                        }

                        //if (perfil.Equals("Asesor"))
                        //{
                        //    return RedirectToAction("Index", "Asesor");
                        //}

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = (string)respIniciarSesion.message;
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    // reCAPTCHA no válido, muestra un mensaje de error
                    return Content("Error: reCAPTCHA no válido.");
                }
            }
        }

        public void CrearClaims(string accessToken, string rol, string correo)
        {
            var accessTokenClaim = new Claim("AccessToken", accessToken);
            /*Claims: son piezas de información de la identidad del usuario en el contexto
             de authentication y authorization*/
            var claims = new List<Claim>
            {
                accessTokenClaim,
                new Claim(ClaimTypes.Role, rol),
                new Claim(ClaimTypes.Email, correo),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            /*HttpContext.SignInAsync sirve para crear la coockie y persistirla en el sistema*/
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        }

        public void eliminarClaims()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectPermanent("/");
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
