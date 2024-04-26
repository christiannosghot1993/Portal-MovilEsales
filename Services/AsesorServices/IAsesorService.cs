using Portal_MovilEsales.Services.AsesorServices.ViewModels;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DatosCliente;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DetallePedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.EstadoCuenta;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.FamilaProducto;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido.GuardarPedidoBorrador;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido.ProcesoFrujoAprobacion;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido.SimulacionPedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.PedidoAprobado;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.PoliticaComercial;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.ProductoExcel;

namespace Portal_MovilEsales.Services.AsesorServices
{
    public interface IAsesorService
    {
        public InicioAsesor getInfoInicioAsesor(string token, string cadena);
        public DatosCliente getDatosCliente(string token, string codigoSapCliente);
        public PedidoAprobado getPedidoAprobado(string token, string codigoPedido);
        public ListadoPedidosBPH getListadoPedidosBPH(string token, string tipoPedido, DateTime fechaInicio, DateTime fechaFin, string cadena);
        public EstadoCuenta getInfoEstadoCuenta(string token, string codigoSAPCliente);
        public InformacionCrediticia getInformacionCrediticia(string token, string codigoSAPCliente);
        public DetallePedidoInfo getDetallePedido(string token, string numeroOrden);
        public CargaCabeceraPedido getCargaCabeceraPedido(string token, string codigoSAPCliente);
        public List<FamiliaProducto> getFamiliaProductos(string token);
        //public List<ProductoPorFamilia> getProductosPorFamilia(string token, string familia);
        public ListadoProductosFavoritos getProductosPorFamilia(string token, string familia, string codigoSAPCliente);
        public ListadoProductosFavoritos getProductosFavoritos(string token, string codigoCliente);
        public ProductoCodigoSap getProductoCodigoSap(string token, string codigoArticulo, string codigoSapCliente);
        public SimulacionPedido getSimulacionPedido(string token, string parametrosPeticion);
        public GuardarPedidoBorrador postGuardarPedidoBorrador(string token, string parametrosPeticion);
        public ProcesoFlujoAprobacion postProcesoFlujoAprobacion(string token, string parametrosPeticion);
        public ProductoExcel getProductosExcel(string token, ProductoExcelRequest prod);
        public List<StockArticulo> getStockArticulos(string token, string codigoSap);
        public string savePoliticaComercial(string token, DetallePolitica detallePolitica);
    }
}
