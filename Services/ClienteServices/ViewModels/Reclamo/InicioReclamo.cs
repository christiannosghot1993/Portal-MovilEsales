using Portal_MovilEsales.Services.ClienteServices.ViewModels.Inicio;

namespace Portal_MovilEsales.Services.ClienteServices.ViewModels.Reclamo
{
    public class InicioReclamo
    {
        public string contactoWhatsApp { get; set; }
        public string nombreCliente { get; set; }
        public string correo { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
        public List<ListadoReclamos> listadoReclamos { get; set; }
        public List<ListadoImagenes> listadoImagenes { get; set; }
    }
}
