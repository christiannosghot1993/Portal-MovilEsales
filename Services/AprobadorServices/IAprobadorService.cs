using Portal_MovilEsales.Services.AprobadorServices.ViewModels.DetallePedidoAprobador;
using Portal_MovilEsales.Services.AprobadorServices.ViewModels.InicioAprobador;

namespace Portal_MovilEsales.Services.AprobadorServices
{
    public interface IAprobadorService
    {
        public InicioAprobador getInfoInicioAprobador(string token);

        public DetallePedidoAprobador getDetallePedidoAprobador(string token, string numeroOrden);
    }
}
