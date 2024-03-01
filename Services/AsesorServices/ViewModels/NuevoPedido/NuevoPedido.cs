using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.FamilaProducto;

namespace Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido
{
    public class NuevoPedido
    {
        public CargaCabeceraPedido cargaCabeceraPedido { get; set; } = new ();

        public List<FamiliaProducto> listaFamiliaProductos { get; set; }
        public ListadoProductosFavoritos listadProductosFavoritos { get; set; }

    }
}
