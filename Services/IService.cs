using Portal_MovilEsales.Models;

namespace Portal_MovilEsales.Services
{
    public interface IService
    {
        dynamic iniciarSesion(IniciarSesionRequest datosLogin);
    }
}
