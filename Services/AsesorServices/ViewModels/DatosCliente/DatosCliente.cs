namespace Portal_MovilEsales.Services.AsesorServices.ViewModels.DatosCliente
{
    public class DatosCliente
    {
        public string codigoSAP { get; set; }
        public string nombreCliente { get; set; }
        public string direccionContacto { get; set; }
        public string telefono { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
        public string correoElectronico { get; set; }
        public DateTime ultimaActualizacion { get; set; }
        public string totalFacturado { get; set; }
        public string totalVencido { get; set; }
        public string cuentasPorCobrar { get; set; }
        public string objetivoCobroMes { get; set; }
        public List<ListadoFacturas> listadoFacturas { get; set; }
        public List<ListadoPedidos> listadoPedidos { get; set; }
        public PedidoAprobado.PedidoAprobado pedidoAprobado { get; set; }
        public InformacionCrediticia informacionCrediticia { get; set; }
    }
}
