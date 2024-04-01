namespace Portal_MovilEsales.Services.AdministradorServices.ViewModels.DetalleReclamo
{
    public class DatosComplementariosReclamo
    {
        public int codigoReclamo { get; set; }

        public int codigoDepartamento { get; set; }

        public int diasParaResolver { get; set; }

        public string correosANotificar { get; set; }
        public string estado { get; set; }

        public string observacion { get; set; }

        public List<ListaEstados> listaEstados { get; set; }
    }
}
