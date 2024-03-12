namespace Portal_MovilEsales.Services.ClienteServices.ViewModels.NuevoPedido
{
    public class CargaCabeceraPedido
    {
        public string nombreCliente { get; set; }

        public string codigoSAP { get; set; }

        public List<ListadoTipoEntrega> listadoTipoEntrega { get; set; } = new();

        public List<ListadoTipoPago> listadoTipoPago { get; set; } = new();

        public List<ListadoDireccionEntrega> listadoDireccionEntrega { get; set; } = new();

        public List<ListadoCanal> listadoCanal { get; set; } = new();

    }
}
