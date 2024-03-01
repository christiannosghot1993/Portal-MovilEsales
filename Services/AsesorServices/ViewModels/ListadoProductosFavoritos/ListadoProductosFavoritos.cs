namespace Portal_MovilEsales.Services.AsesorServices.ViewModels
{
    public class ListadoProductosFavoritos
    {
        public bool success { get; set; }
        public string resultcode { get; set; }
        public string message { get; set; }
        public List<Result> result { get; set; }
    }
}
