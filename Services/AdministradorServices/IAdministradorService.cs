using Portal_MovilEsales.Services.AdministradorServices.ViewModels;
using Portal_MovilEsales.Services.AdministradorServices.ViewModels.DetalleReclamo;
using Portal_MovilEsales.Services.AdministradorServices.ViewModels.InicioAdministrador;

namespace Portal_MovilEsales.Services.AdministradorServices
{
    public interface IAdministradorService
    {
        public InicioAdministrador getInfoInicioAdministrador(string token);

        public DetalleReclamo getDetalleReclamo(string token, int codigoReclamo);

        public DatosComplementariosReclamo postDatosComplementariosReclamo(string token, int codigoReclamo, int codigoDepartamento, int diasParaResolver, string correosANotificar, string estado, string observacion);

        public ReclamoIndividual getReclamoIndividual(string token, int codigoReclamo);

        public Acciones postAccionesRealizadas(string token, int codigoReclamo, string estado, string accion, string observaciones, string archivoBase64);
    }
}
