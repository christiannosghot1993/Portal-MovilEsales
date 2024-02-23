namespace Portal_MovilEsales.Services.AsesorServices.ViewModels
{
    public class InicioAsesor
    {
        public string contactoWhatsApp { get; set; }
        public string nombreAsesor { get; set; }
        public string correo { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
        public List<ListadoClientes> listadoClientes { get; set; }
        public List<ListadoPedidos> listadoPedidos { get; set; }
        public List<ListadoImagenes> listadoImagenes { get; set; }
    }
}
