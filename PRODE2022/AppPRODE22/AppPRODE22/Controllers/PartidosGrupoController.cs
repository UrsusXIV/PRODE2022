using AppPRODE22.Controllers.DTOs; // Importa los Data Transfer Objects (DTOs) necesarios para la transferencia de datos entre el frontend y el backend.
using AppPRODE22.Repository; // Importa el manejador de datos que realiza operaciones de base de datos relacionadas con los partidos de grupo.
using Microsoft.AspNetCore.Mvc; // Importa los componentes de ASP.NET Core necesarios para la construcción de la API.

namespace AppPRODE22.Controllers // Define el espacio de nombres del controlador de la API.
{
    [ApiController] // Indica que esta clase es un controlador de API.
    [Route("[controller]")] // Define la ruta base para todas las solicitudes manejadas por este controlador.

    public class PartidosGrupoController
    {
        [HttpPost] // Define que este método maneja solicitudes HTTP POST.
        public bool altaPartidosGrupo([FromBody] PostPartidosGruposDTO altaPartidosGrupoBody)
        {
            // Llama al manejador para agregar un nuevo partido de grupo utilizando los datos proporcionados en el cuerpo de la solicitud.
            return PartidosGrupoHandler.altaPartidosGrupoHandler(altaPartidosGrupoBody);
        }

        [HttpGet] // Define que este método maneja solicitudes HTTP GET.
        public PartidosGruposResponse consultaPartidosGrupo([FromQuery] GetPartidosGruposDTO consultaPartidosGrupoBody)
        {
            // Llama al manejador para consultar partidos de grupo según los parámetros proporcionados en la consulta de la URL.
            return PartidosGrupoHandler.consultaPartidosGrupoHandler(consultaPartidosGrupoBody);
        }

        [HttpPut] // Define que este método maneja solicitudes HTTP PUT.
        public bool modificacionPartidosGrupo([FromBody] PutPartidosGruposDTO modificacionPartidosGrupoBody)
        {
            // Llama al manejador para modificar un partido de grupo utilizando los datos proporcionados en el cuerpo de la solicitud.
            return PartidosGrupoHandler.modificacionPartidosGrupoHandler(modificacionPartidosGrupoBody);
        }

        [HttpDelete] // Define que este método maneja solicitudes HTTP DELETE.
        public bool bajaPartidosGrupo([FromBody] DeletePartidosGruposDTO bajaPartidosGrupoBody)
        {
            // Llama al manejador para eliminar un partido de grupo utilizando los datos proporcionados en el cuerpo de la solicitud.
            return PartidosGrupoHandler.bajaPartidosGrupoHandler(bajaPartidosGrupoBody);
        }
    }
}
