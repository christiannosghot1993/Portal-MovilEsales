namespace Portal_MovilEsales.Services.AdministradorServices.ViewModels.DetalleReclamo
{
    public class ReclamoIndividual
    {
        public int codigoReclamo { get; set; }
        public string motivo { get; set; }
        public string factura { get; set; }
        public string producto { get; set; }
        public string referencia { get; set; }
        public string asunto { get; set; }
        public string observacionesDatosOriginales { get; set; }
        public string fotografiaMaterial { get; set; }
        public string copiaFactura { get; set; }
        public string departamento { get; set; }
        public int diasParaResolver { get; set; }
        public string correosANotificar { get; set; }
        public string estado { get; set; }
        public string observacionesDatosComplementarios { get; set; }
        public List<ListaEstados> listaEstados { get; set; }
        public List<ListaAcciones> listaAcciones { get; set; }
    }
}
