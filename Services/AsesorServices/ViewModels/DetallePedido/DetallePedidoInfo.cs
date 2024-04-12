namespace Portal_MovilEsales.Services.AsesorServices.ViewModels.DetallePedido
{
    public class DetallePedidoInfo
    {
        public string nombreCliente { get; set; }

        public string formaPago { get; set; }

        public string direccionEntrega { get; set; }

        public string totalPeso { get; set; }

        public string observacion { get; set; }

        public string contacto { get; set; }

        public string direccion { get; set; }

        public string fechaProceso { get; set; }

        public string numeroOrden { get; set; }

        public string estado { get; set; }

        public string numeroOrdenSAP { get; set; }

        public string ordenCompra { get; set; }

        public string fechaEntrega { get; set; }

        public List<ListadoDetallePedido> detallePedido { get; set; }

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

        public string margenVal { get; set; }

        public string MensajeServicio { get; set; }
    }
}
