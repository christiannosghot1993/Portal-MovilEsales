

using Portal_MovilEsales.Services.AsesorServices.ViewModels;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DatosCliente;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.PedidoAprobado;

namespace Portal_MovilEsales.Services.AsesorServices
{
    public interface IAsesorService
    {
        public InicioAsesor getInfoInicioAsesor(string token);
        public DatosCliente getDatosCliente(string token, string codigoSapCliente);
        public PedidoAprobado getPedidoAprobado(string token, string codigoPedido);
    }
}
