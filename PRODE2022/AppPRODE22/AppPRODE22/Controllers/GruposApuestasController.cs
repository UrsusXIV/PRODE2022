using AppPRODE22.Controllers.DTOs; // Importa los Data Transfer Objects (DTOs) necesarios para la transferencia de datos entre el frontend y el backend.
using AppPRODE22.Repository; // Importa el manejador de datos que realiza operaciones de base de datos relacionadas con los grupos de apuestas.
using Microsoft.AspNetCore.Mvc; // Importa los componentes de ASP.NET Core necesarios para la construcción de la API.

namespace AppPRODE22.Controllers // Define el espacio de nombres del controlador de la API.
{
    [ApiController] // Indica que esta clase es un controlador de API.
    [Route("[controller]")] // Define la ruta base para todas las solicitudes manejadas por este controlador.
    public class GruposApuestasController
    {
        [HttpPost] // Define que este método maneja solicitudes HTTP POST.
        public bool altaGruposApuestas([FromBody] PostGruposApuestasDTO altaGruposApuestasBody)
        {
            // Llama al manejador para agregar un nuevo grupo de apuestas utilizando los datos proporcionados en el cuerpo de la solicitud.
            return GruposApuestasHandler.altaGruposApuestasHandler(altaGruposApuestasBody);
        }

        [HttpGet] // Define que este método maneja solicitudes HTTP GET.
        public GrupoApuestasResponse consultaGrupoApuestas([FromQuery] GetGruposApuestasDTO consultaGrupoApuestasQuery)
        {
            // Llama al manejador para consultar grupos de apuestas según los parámetros proporcionados en la consulta de la URL.
            return GruposApuestasHandler.consultaGruposApuestasHandler(consultaGrupoApuestasQuery);
        }

        [HttpPut] // Define que este método maneja solicitudes HTTP PUT.
        public bool modificacionGruposApuestas([FromBody] PutGruposApuestasDTO modificacionGruposApuestasBody)
        {
            // Llama al manejador para modificar un grupo de apuestas utilizando los datos proporcionados en el cuerpo de la solicitud.
            return GruposApuestasHandler.modificacionGruposApuestasHandler(modificacionGruposApuestasBody);
        }

        [HttpDelete] // Define que este método maneja solicitudes HTTP DELETE.
        public bool bajaGruposApuestas([FromBody] DeleteGruposApuestasDTO bajaGruposApuestasBody)
        {
            // Llama al manejador para eliminar un grupo de apuestas utilizando los datos proporcionados en el cuerpo de la solicitud.
            return GruposApuestasHandler.bajaGruposApuestasHandler(bajaGruposApuestasBody);
        }
    }
}
