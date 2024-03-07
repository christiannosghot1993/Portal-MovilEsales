namespace Portal_MovilEsales.Services.AprobadorServices.ViewModels.InicioAprobador
{
    public class InicioAprobador
    {
        public string contactoWhatsApp { get; set; }

        public string nombreAprobador { get; set; }

        public string correo { get; set; }

        public string ciudad { get; set; }

        public string pais { get; set; }

        public List<ListadoPedidosAprobador> listadoPedidos { get; set; }

        public DetallePedidoAprobador.DetallePedidoAprobador detallePedidoPendiente { get; set; }
    }
}
