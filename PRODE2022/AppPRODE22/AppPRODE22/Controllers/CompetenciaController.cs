using AppPRODE22.Controllers.DTOs; // Importa los Data Transfer Objects (DTOs) necesarios para la transferencia de datos entre el frontend y el backend.
using AppPRODE22.Repository; // Importa el manejador de datos que realiza operaciones de base de datos relacionadas con las competencias.
using Microsoft.AspNetCore.Mvc; // Importa los componentes de ASP.NET Core necesarios para la construcción de la API.

namespace AppPRODE22.Controllers // Define el espacio de nombres del controlador de la API.
{
    [ApiController] // Indica que esta clase es un controlador de API.
    [Route("[controller]")] // Define la ruta base para todas las solicitudes manejadas por este controlador.
    public class CompetenciaController
    {
        [HttpPost] // Define que este método maneja solicitudes HTTP POST.
        public bool altaCompetencia([FromBody] PostCompetenciasDTO altaCompetenciaBody)
        {
            // Llama al manejador para agregar una nueva competencia utilizando los datos proporcionados en el cuerpo de la solicitud.
            return CompetenciaHandler.altaCompetenciaHandler(altaCompetenciaBody);
        }

        [HttpGet] // Define que este método maneja solicitudes HTTP GET.
        public CompetenciasResponse consultaCompetencias([FromQuery] GetCompetenciasDTO consultaCompetenciasBody)
        {
            // Llama al manejador para consultar competencias según los criterios proporcionados en la consulta de la URL.
            return CompetenciaHandler.consultaCompetenciasHandler(consultaCompetenciasBody);
        }

        [HttpPut] // Define que este método maneja solicitudes HTTP PUT.
        public bool modificacionCompetencia([FromBody] PutCompetenciaDTO modificacionCompetenciaBody)
        {
            // Llama al manejador para modificar una competencia existente utilizando los datos proporcionados en el cuerpo de la solicitud.
            return CompetenciaHandler.modificacionCompetenciaHandler(modificacionCompetenciaBody);
        }

        [HttpDelete] // Define que este método maneja solicitudes HTTP DELETE.
        public bool bajaCompetencia([FromBody] DeleteCompetenciasDTO bajaCompetenciaBody)
        {
            // Llama al manejador para eliminar una competencia utilizando los datos proporcionados en el cuerpo de la solicitud.
            return CompetenciaHandler.bajaCompetenciaHandler(bajaCompetenciaBody);
        }
    }
}
