namespace Portal_MovilEsales.Services.AsesorServices.ViewModels
{
    public class ListadoPedidosBPH
    {
        public bool success { get; set; }
        public string resultcode { get; set; }
        public string message { get; set; }
        public List<InfoPedidoBPH> result { get; set; }
        public DetallePedido.DetallePedidoInfo detallePedidoInfo { get; set; }
        public ProcesoRespuesta.ProcesoRespuesta procesoRespuesta { get; set; } = new();
    }
}
