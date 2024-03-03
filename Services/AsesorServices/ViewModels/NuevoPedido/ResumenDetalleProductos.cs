namespace Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido
{
    public class ResumenDetalleProductos
    {
        public string importeBruto { get; set; }

        public string descuentoBase { get; set; }

        public string subTotal1 { get; set; }

        public string descuentoPago { get; set; }

        public string descuentoRetiro { get; set; }

        public string descuentoVarios { get; set; }

        public string descuentoPeso { get; set; }

        public string subTotal2 { get; set; }

        public string iva { get; set; }

        public string seguroTransporte { get; set; }

        public string valorTotal { get; set; }

        public string valorNcsinIva { get; set; }

        public string margenPor { get; set; }

        //public ProcesoRespuesta.ProcesoRespuesta procesoRespuesta { get; set; } = new();
    }
}
