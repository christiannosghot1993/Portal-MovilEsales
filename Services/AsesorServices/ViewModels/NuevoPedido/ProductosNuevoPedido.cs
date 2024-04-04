namespace Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido
{
    public class ProductosNuevoPedido
    {
        public bool isChecked { get; set; } = false;
        public string numeroRegistro {  get; set; }

        public bool bloqueado { get; set; } = false;

        public string codigo { get; set; }

        public string descripcion { get; set; }

        //public List<ListadoTipoEntrega> listadoTipoEntregas { get; set; }
        public List<ListadoBodegas> listadoBodegas { get; set; }

        public string BodegaProductoSel { get; set; }

        public string unidad { get; set; }

        public string peso { get; set; }

        public string descFac {  get; set; }
        
        public string descNc { get; set; }

        public string idl { get; set; }

        public string subtotal {  get; set; }

        public string cantidad {  get; set; }

        public bool aFinMes { get; set; } = false;

        public bool aFamilia { get; set; } = false;

        //nuevos campos SB marzo 2024
        public string precio { get; set; }
        public string descBase { get; set; }
        public string precioFinal { get; set; }
        public string subtotal2 { get; set; }
    }
}
