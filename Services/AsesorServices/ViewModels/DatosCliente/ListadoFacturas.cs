namespace Portal_MovilEsales.Services.AsesorServices.ViewModels.DatosCliente
{
    public class ListadoFacturas
    {
        public string numeroFactura { get; set; }
        public string monto { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int diasMora { get; set; }
    }
}
