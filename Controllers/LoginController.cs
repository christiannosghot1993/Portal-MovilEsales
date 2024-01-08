using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal_MovilEsales.Models;

namespace Portal_MovilEsales.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcesarFormulario([FromForm(Name = "g-recaptcha-response")] string gRecaptchaResponse)
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // reCAPTCHA no válido, muestra un mensaje de error
                    return Content("Error: reCAPTCHA no válido.");
                }
            }
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
