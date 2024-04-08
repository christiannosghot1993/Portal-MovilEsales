using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido;

namespace Portal_MovilEsales.Services.ClienteServices.ViewModels.NuevoPedido
{
    public class ProductosNuevoPedido
    {
        
        public string numeroRegistro {  get; set; }

        public bool bloqueado { get; set; } = false;

        public string codigo { get; set; }

        public string descripcion { get; set; }

        //public List<ListadoTipoEntrega> listadoTipoEntregas { get; set; }

        public List<ListadoBodegas> listadoBodegas { get; set; }
        public string unidad { get; set; }

        public string peso { get; set; }

        public string descFac {  get; set; }
        
        public string descNc { get; set; }

        public string idl { get; set; }

        public string subtotal {  get; set; }

        public string cantidad {  get; set; }

        public bool aFinMes { get; set; } = false;

        public bool aFamilia { get; set; } = false;
    }
}
