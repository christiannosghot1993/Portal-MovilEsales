using Portal_MovilEsales.Services.ClienteServices.ViewModels.Inicio;

namespace Portal_MovilEsales.Services.ClienteServices.ViewModels.Reclamo
{
    public class ReclamoEnProgreso
    {
        public int codigoReclamo { get; set; }
        public string fechaReclamo { get; set; }
        public string observaciones { get; set; }
        public string estado { get; set; }
        public string motivo { get; set; }
        public string producto { get; set; }
        public string factura { get; set; }
        public string referencia { get; set; }
        public string asunto { get; set; }
        public string linkFotografiaMaterial { get; set; }
        public string copiaFactura { get; set; }
        public string respuestaACliente { get; set; }
        public string archivoSoporte { get; set; }
        public string fechaRespuesta { get; set; }
        public string comentarioCliente { get; set; }
        public int calificacionServicio { get; set; }

        public List<ListadoImagenes> listadoImagenes { get; set; }
    }
}
