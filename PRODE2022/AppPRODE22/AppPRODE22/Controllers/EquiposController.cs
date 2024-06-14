using AppPRODE22.Controllers.DTOs; // Importa los Data Transfer Objects (DTOs) necesarios para la transferencia de datos entre el frontend y el backend.
using AppPRODE22.Repository; // Importa el manejador de datos que realiza operaciones de base de datos relacionadas con los equipos.
using Microsoft.AspNetCore.Mvc; // Importa los componentes de ASP.NET Core necesarios para la construcción de la API.

namespace AppPRODE22.Controllers // Define el espacio de nombres del controlador de la API.
{
    [ApiController] // Indica que esta clase es un controlador de API.
    [Route("[controller]")] // Define la ruta base para todas las solicitudes manejadas por este controlador.

    public class EquiposController
    {
        [HttpPost] // Define que este método maneja solicitudes HTTP POST.
        public bool altaEquipo([FromBody] PostEquipoDTO altaEquiposBody)
        {
            // Llama al manejador para agregar un nuevo equipo utilizando los datos proporcionados en el cuerpo de la solicitud.
            return EquiposHandler.altaEquipoHandler(altaEquiposBody);
        }

        [HttpGet] // Define que este método maneja solicitudes HTTP GET.
        public EquiposResponse consultaEquipos([FromQuery] GetEquipoDTO consultaEquiposBody)
        {
            // Llama al manejador para consultar equipos según los criterios proporcionados en la consulta de la URL.
            return EquiposHandler.consultaEquiposHandler(consultaEquiposBody);
        }

        [HttpPut] // Define que este método maneja solicitudes HTTP PUT.
        public bool modificacionEquipos([FromBody] PutEquipoDTO modificacionEquipoBody)
        {
            // Llama al manejador para modificar un equipo existente utilizando los datos proporcionados en el cuerpo de la solicitud.
            return EquiposHandler.modificacionEquiposHandler(modificacionEquipoBody);
        }

        [HttpDelete] // Define que este método maneja solicitudes HTTP DELETE.
        public bool bajaEquipos([FromBody] DeleteEquipoDTO bajaEquipoBody)
        {
            // Llama al manejador para eliminar un equipo utilizando los datos proporcionados en el cuerpo de la solicitud.
            return EquiposHandler.bajaEquipoHandler(bajaEquipoBody);
        }
    }
}
