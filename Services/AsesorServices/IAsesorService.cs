

using Portal_MovilEsales.Services.AsesorServices.ViewModels;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DatosCliente;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.DetallePedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.EstadoCuenta;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.FamilaProducto;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.NuevoPedido;
using Portal_MovilEsales.Services.AsesorServices.ViewModels.PedidoAprobado;

namespace Portal_MovilEsales.Services.AsesorServices
{
    public interface IAsesorService
    {
        public InicioAsesor getInfoInicioAsesor(string token);
        public DatosCliente getDatosCliente(string token, string codigoSapCliente);
        public PedidoAprobado getPedidoAprobado(string token, string codigoPedido);
        public ListadoPedidosBPH getListadoPedidosBPH(string token, string tipoPedido, DateTime fechaInicio, DateTime fechaFin, string cadena);
        public EstadoCuenta getInfoEstadoCuenta(string token, string codigoSAPCliente);
        public InformacionCrediticia getInformacionCrediticia(string token, string codigoSAPCliente);
        public DetallePedidoInfo getDetallePedido(string token, string numeroOrden);
        public CargaCabeceraPedido getCargaCabeceraPedido(string token, string codigoSAPCliente);
        public List<FamiliaProducto> getFamiliaProductos(string token);
    }
}
