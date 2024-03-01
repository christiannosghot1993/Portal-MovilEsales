
namespace Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido.ProcesoFrujoAprobacion
{
    public class ProcesoFlujoAprobacion
    {
        public string nombreCliente { get; set; }

        public string formaPago { get; set; }

        public string direccionEntrega { get; set; }

        public string totalPeso { get; set; }

        public string observacion { get; set; }

        public string contacto { get; set; }

        public string direccion { get; set; }

        public string fechaProceso { get; set; }

        public string numeroOrden { get; set; }

        public string estado { get; set; }

        public string numeroOrdenSAP { get; set; }

        public string fechaEntrega { get; set; }

        public string ordenCompra { get; set; }

        public List<ListadoProductoProcesoFlujoAprobacion> detallePedido { get; set; }
    }
}
