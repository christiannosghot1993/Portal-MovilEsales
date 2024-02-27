namespace Portal_MovilEsales.Services.AsesorServices.ViewModels.EstadoCuenta
{
    public class EstadoCuenta
    {
        public string nombreCliente { get;set; } 

        public string valorVencido { get; set; }

        public string valorPorVencer { get; set; }
        
        public List<ListadoEstadoCuenta> detalleEstadoCuenta { get; set; }
    }
}
