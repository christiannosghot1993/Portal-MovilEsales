namespace Portal_MovilEsales.Services.ClienteServices.ViewModels.PedidoAprobado
{
    public class PedidoAprobado
    {
        public string codigoBodega { get; set; }
        public string observacion { get; set; }
        public string ordenPedido { get; set; }
        public DateTime fechaPedido { get; set; }
        public string formaPago { get; set; }
        public string direccionEntrega { get; set; }
        public double pesoTotal { get; set; }
        public List<DetallePedido> detallePedido { get; set; }
    }
}
