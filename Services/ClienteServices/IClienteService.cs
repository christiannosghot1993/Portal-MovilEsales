using Portal_MovilEsales.Services.AsesorServices.ViewModels.EstadoCuenta;
using Portal_MovilEsales.Services.ClienteServices.ViewModels;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.DetallePedido;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.FamilaProducto;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.Inicio;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.NuevoPedido;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.NuevoPedido.GuardarPedidoBorrador;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.NuevoPedido.ProcesoFrujoAprobacion;
using Portal_MovilEsales.Services.ClienteServices.ViewModels.PedidoAprobado;

namespace Portal_MovilEsales.Services.ClienteServices
{
    public interface IClienteService
    {
        public InicioCliente getInfoInicioCliente(string token);

        public InformacionCrediticia getInformacionCrediticia(string token, string codigoSAPCliente);

        public PedidoAprobado getPedidoAprobado(string token, string codigoPedido);

        public ListadoPedidosBPH getListadoPedidosBPH(string token, string tipoPedido, DateTime fechaInicio, DateTime fechaFin, string cadena);

        public DetallePedidoInfo getDetallePedido(string token, string numeroOrden);

        public CargaCabeceraPedido getCargaCabeceraPedido(string token, string codigoSAPCliente);

        public List<FamiliaProducto> getFamiliaProductos(string token);
        
        public ListadoProductosFavoritos getProductosPorFamilia(string token, string familia);

        public ListadoProductosFavoritos getProductosFavoritos(string token, string codigoCliente);

        public GuardarPedidoBorrador postGuardarPedidoBorrador(string token, string parametrosPeticion);

        public ProcesoFlujoAprobacion postProcesoFlujoAprobacion(string token, string parametrosPeticion);

        public ProductoCodigoSap getProductoCodigoSap(string token, string codigoArticulo, string codigoSapCliente);
        public EstadoCuenta getInfoEstadoCuenta(string token, string codigoSAPCliente);
    }
}
