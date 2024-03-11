
namespace Portal_MovilEsales.Services.ClienteServices.ViewModels.Inicio
{
    public class InicioCliente
    {
        public string codigoSAP { get; set; }
        public string nombreCliente { get; set; }
        public string direccionContacto { get; set; }
        public string telefono { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
        public string correoElectronico { get; set; }
        public DateTime ultimaActualizacion { get; set; }
        public double totalFacturado { get; set; }
        public double totalVencido { get; set; }
        public double cuentasPorCobrar { get; set; }
        public double objetivoCobroMes { get; set; }
        public List<ListadoFacturasCliente> listadoFacturas { get; set; }
        public List<ListadoPedidosCliente> listadoPedidos { get; set; }
        //public PedidoAprobado.PedidoAprobado pedidoAprobado { get; set; }
        public InformacionCrediticia informacionCrediticia { get; set; }
    }
}
