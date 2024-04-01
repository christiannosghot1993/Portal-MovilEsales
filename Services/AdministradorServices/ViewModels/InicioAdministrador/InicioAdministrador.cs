namespace Portal_MovilEsales.Services.AdministradorServices.ViewModels.InicioAdministrador
{
    public class InicioAdministrador
    {
        public string contactoWhatsApp { get; set; }
        public string nombreAdministrador { get; set; }
        public string correo { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }

        public List<ListadoPedidos> listadoPedidos { get; set; }

        public List<ListadoReclamos> listadoReclamos { get; set; }
    }
}
