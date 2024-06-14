using AppPRODE22.Controllers.DTOs; // Importa los Data Transfer Objects (DTOs) necesarios para la transferencia de datos entre el frontend y el backend.
using AppPRODE22.Repository; // Importa el manejador de datos que realiza operaciones de base de datos relacionadas con equipos por competencia.
using Microsoft.AspNetCore.Mvc; // Importa los componentes de ASP.NET Core necesarios para la construcción de la API.

namespace AppPRODE22.Controllers // Define el espacio de nombres del controlador de la API.
{
    [ApiController] // Indica que esta clase es un controlador de API.
    [Route("[controller]")] // Define la ruta base para todas las solicitudes manejadas por este controlador.

    public class EquiposXCompetenciaController
    {
        [HttpPost] // Define que este método maneja solicitudes HTTP POST.
        // Da de alta los equipos por competencia.
        public bool altaEqpsXComp([FromBody] PostEquiposXCompetenciaDTO altaEqpsXCompBody)
        {
            // Llama al manejador para agregar equipos por competencia utilizando los datos proporcionados en el cuerpo de la solicitud.
            return EquiposXCompetenciaHandler.altaEqpsXCompHandler(altaEqpsXCompBody);
        }

        [HttpGet] // Define que este método maneja solicitudes HTTP GET.
        public EquiposXCompetenciaResponse consultaEquiposXCompetencia([FromQuery] GetEquiposXCompetenciaDTO consultaEquiposXCompetenciaBody)
        {
            // Llama al manejador para consultar equipos por competencia según los criterios proporcionados en la consulta de la URL.
            return EquiposXCompetenciaHandler.consultaEquiposXCompetenciaHandler(consultaEquiposXCompetenciaBody);
        }

        [HttpPut] // Define que este método maneja solicitudes HTTP PUT.
        public bool modificacionEqpsXComp([FromBody] PutEquiposXCompetenciaDTO modificacionEqpsXCompBody)
        {
            // Llama al manejador para modificar equipos por competencia utilizando los datos proporcionados en el cuerpo de la solicitud.
            return EquiposXCompetenciaHandler.modificacionEqpsXCompHandler(modificacionEqpsXCompBody);
        }

        [HttpDelete] // Define que este método maneja solicitudes HTTP DELETE.
        public bool bajaEqpsXComp([FromBody] DeleteEquiposXCompetenciaDTO bajaEqpsXCompBody)
        {
            // Llama al manejador para eliminar equipos por competencia utilizando los datos proporcionados en el cuerpo de la solicitud.
            return EquiposXCompetenciaHandler.bajaEqpsXCompHandler(bajaEqpsXCompBody);
        }
    }
}
