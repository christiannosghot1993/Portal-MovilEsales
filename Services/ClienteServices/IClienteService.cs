using Portal_MovilEsales.Services.ClienteServices.ViewModels.Inicio;

namespace Portal_MovilEsales.Services.ClienteServices
{
    public interface IClienteService
    {
        public InicioCliente getInfoInicioCliente(string token);

        public InformacionCrediticia getInformacionCrediticia(string token, string codigoSAPCliente);
    }
}
