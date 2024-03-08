namespace Portal_MovilEsales.Services.AprobadorServices.ViewModels.PedidosReporte
{
    public class ListadoPedidosReporte
    {
        public bool success { get; set; }
        public string resultcode { get; set; }
        public string message { get; set; }
        public List<InfoPedidoReporte> result { get; set; } = new List<InfoPedidoReporte>();
    }
}
