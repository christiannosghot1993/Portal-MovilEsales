using Portal_MovilEsales.Services.AprobadorServices.ViewModels.DetallePedidoAprobador;
using Portal_MovilEsales.Services.AprobadorServices.ViewModels.InicioAprobador;
using Portal_MovilEsales.Services.AprobadorServices.ViewModels.PedidosReporte;
using Portal_MovilEsales.Services.AprobadorServices.ViewModels.ProcesaPedido;

namespace Portal_MovilEsales.Services.AprobadorServices
{
    public interface IAprobadorService
    {
        public InicioAprobador getInfoInicioAprobador(string token);

        public DetallePedidoAprobador getDetallePedidoAprobador(string token, string numeroOrden);

        public ProcesaPedido postProcesaPedido(string token, string numeroOrden, string aprobacionDescuento,
                                               string aprobacionCondicionesEspeciales, string aprobacionNotaCredito,
                                               string aprobacionGeneral, string mensaje);

        public ListadoPedidosReporte getListadoPedidosReporte(string token, string fechaInicio, string fechaFin, string orden, 
                                                              string cliente, string asesor, string estado);
    }
}
