namespace Portal_MovilEsales.Services.AprobadorServices.ViewModels.PedidosReporte
{
    public class ReportePedido
    {
        public ListadoPedidosReporte listadoPedidosReporte {  get; set; } = new ListadoPedidosReporte();

        public DetallePedidoAprobador.DetallePedidoAprobador detallePedidoReporte { get; set; } = new();
    }
}
