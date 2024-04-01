namespace Portal_MovilEsales.Services.AdministradorServices.ViewModels.DetalleReclamo
{
    public class DetalleReclamo
    {
        public int codigoReclamo { get; set; }
        public string motivo { get; set; }
        public string factura { get; set; }
        public string producto { get; set; }
        public string referencia { get; set; }
        public string asunto { get; set; }
        public string observaciones { get; set; }
        public string fotografiaMaterial { get; set; }
        public string copiaFactura { get; set; }
        public int codigoDepartamento { get; set; }
        public string estadoActual { get; set; }

        public List<ListadoDepartamentos> listadoDepartamentos { get; set; }
        public List<ListaEstados> listaEstados { get; set; }
    }
}
