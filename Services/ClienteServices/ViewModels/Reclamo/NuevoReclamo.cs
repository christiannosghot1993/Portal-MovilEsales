using Portal_MovilEsales.Services.ClienteServices.ViewModels.Inicio;

namespace Portal_MovilEsales.Services.ClienteServices.ViewModels.Reclamo
{
    public class NuevoReclamo
    {
        public string motivo { get; set; }
        public string factura { get; set; }
        public string producto { get; set; }
        public string referencia { get; set; }
        public string asunto { get; set; }
        public string observaciones { get; set; }
        public string fotografiaMaterial { get; set; }
        public string copiaFactura { get; set; }
        /*public byte[] fotografiaMaterial { get; set; }
        public byte[] copiaFactura { get; set; }*/
        public List<ListadoMotivo> listadoMotivo { get; set; }
        public List<ListadoProductos> listadoProductos { get; set; }
        public List<ListadoImagenes> listadoImagenes { get; set; }
    }
}
