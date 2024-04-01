using Newtonsoft.Json;
using Portal_MovilEsales.Services.AdministradorServices.ViewModels;
using Portal_MovilEsales.Services.AdministradorServices.ViewModels.DetalleReclamo;
using Portal_MovilEsales.Services.AdministradorServices.ViewModels.InicioAdministrador;

namespace Portal_MovilEsales.Services.AdministradorServices
{
    public class AdministradorService : IAdministradorService
    {
        public InicioAdministrador getInfoInicioAdministrador(string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/InicioAdministrador");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\r\n    \"navegadorweb\":\"Microsoft Edge XXX\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);
            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            InicioAdministrador inicioAdministrador;
            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);
                inicioAdministrador = JsonConvert.DeserializeObject<InicioAdministrador>(jsonInfo);
            }
            else
            {
                inicioAdministrador = new InicioAdministrador();
            }
            return inicioAdministrador;
        }

        public DetalleReclamo getDetalleReclamo(string token, int codigoReclamo)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/DetalleReclamo");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\r\n    \"CodigoReclamo\": \"" + codigoReclamo + "\",\r\n    \"navegadorweb\": \"Microsoft Edge XXX\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            DetalleReclamo detalleReclamo;
            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);
                detalleReclamo = JsonConvert.DeserializeObject<DetalleReclamo>(jsonInfo);
            }
            else
            {
                detalleReclamo = new DetalleReclamo();
            }
            return detalleReclamo;

        }

        public DatosComplementariosReclamo postDatosComplementariosReclamo(string token, int codigoReclamo, int codigoDepartamento, int diasParaResolver, string correosANotificar, string estado, string observacion)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/DatosComplementariosReclamo");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
                CodigoReclamo = codigoReclamo,
                CodigoDepartamento = codigoDepartamento,
                DiasParaResolver = diasParaResolver,
                CorreosANotificar = correosANotificar,
                Estado = estado,
                Observacion = observacion
            });

            var content = new StringContent(jsonBody, null, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            DatosComplementariosReclamo datosComplementariosReclamo;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                datosComplementariosReclamo = JsonConvert.DeserializeObject<DatosComplementariosReclamo>(jsonInfo);
            }
            else
            {
                datosComplementariosReclamo = new DatosComplementariosReclamo();
            }

            return datosComplementariosReclamo;
        }

        public ReclamoIndividual getReclamoIndividual(string token, int codigoReclamo)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/DetalleReclamoGlobal");
            request.Headers.Add("Authorization", "Bearer " + token);
            var content = new StringContent("{\r\n    \"CodigoReclamo\": \"" + codigoReclamo + "\",\r\n    \"navegadorweb\": \"Microsoft Edge XXX\"\r\n}", null, "application/json");
            request.Content = content;
            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;
            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);
            ReclamoIndividual reclamoGlobal;
            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);
                reclamoGlobal = JsonConvert.DeserializeObject<ReclamoIndividual>(jsonInfo);
            }
            else
            {
                reclamoGlobal = new ReclamoIndividual();
            }
            return reclamoGlobal;

        }

        public Acciones postAccionesRealizadas(string token, int codigoReclamo, string estado, string accion, string observaciones, string archivoBase64)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://esaleslatam.bekaert.com:9020/esalesapi/api/GrabaAccion");

            request.Headers.Add("Authorization", "Bearer " + token);

            var jsonBody = JsonConvert.SerializeObject(new
            {
                navegadorweb = "Microsoft Edge XXX",
                CodigoReclamo = codigoReclamo,
                estado = estado,
                accion = accion,
                observaciones = observaciones,
                archivoBase64 = archivoBase64
            });

            var content = new StringContent(jsonBody, null, "application/json");

            request.Content = content;

            var response = client.Send(request);

            string resultado = response.Content.ReadAsStringAsync().Result;

            var resDynamic = JsonConvert.DeserializeObject<dynamic>(resultado);

            Acciones acciones;

            if ((bool)resDynamic.success)
            {
                string jsonInfo = JsonConvert.SerializeObject(resDynamic.result);

                acciones = JsonConvert.DeserializeObject<Acciones>(jsonInfo);
            }
            else
            {
                acciones = new Acciones();
            }
            return acciones;
        }
    }
}
