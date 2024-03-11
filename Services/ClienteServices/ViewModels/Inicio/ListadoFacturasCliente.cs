namespace Portal_MovilEsales.Services.ClienteServices.ViewModels.Inicio
{
    public class ListadoFacturasCliente
    {
        public string numeroFactura { get; set; }
        public double monto { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int diasMora { get; set; }
    }
}
