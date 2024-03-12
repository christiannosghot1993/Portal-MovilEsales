namespace Portal_MovilEsales.Services.ClienteServices.ViewModels
{
    public class InfoPedidoBPH
    {
        public string nombreCliente { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string observacion { get; set; }
        public string numeroOrden { get; set; }
        public string ordenSAP { get; set; }
        public double compraKG { get; set; }
        public double compraUSD { get; set; }
        public double valorNCSinIva { get; set; }
        public string aprobadorActual { get; set; }
        public string estado { get; set; }
    }
}
