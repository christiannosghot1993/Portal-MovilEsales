namespace Portal_MovilEsales.Middlewares
{
    using DocumentFormat.OpenXml.InkML;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class RedirectIfAuthenticatedMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectIfAuthenticatedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var perfil = context.Session.GetString("perfil");

            // Verificamos si la página correponde a la del login
            if (context.Request.Path == "/" && perfil is not null)
            {
                // Verificamos si el usuario está autenticado
                if (context.User.Identity.IsAuthenticated)
                {
                    
                    var page = perfil switch
                    {
                        "Asesor" => "Asesor",
                        "Aprobador" => "Aprobador",
                        "Cliente" => "Cliente",
                        "Administardor" => "Administrador",
                        _ => string.Empty
                    };

                    // Redireccionamos a la página adecuada de acuerdo al tipo de perfil
                    context.Response.Redirect($"/{page}");

                    return;
                }
            }

            await _next(context);
        }
    }
}
