using AppPRODE22.Controllers.DTOs; // Importa los Data Transfer Objects (DTOs) que se utilizan para la transferencia de datos entre el frontend y el backend.
using AppPRODE22.Repository; // Importa el manejador de datos necesario para las operaciones de la base de datos relacionadas con las apuestas.
using Microsoft.AspNetCore.Mvc; // Importa los componentes de ASP.NET Core necesarios para la construcción de la API.

namespace AppPRODE22.Controllers // Define el espacio de nombres del controlador de la API.
{
    [ApiController] // Indica que esta clase es un controlador de API.
    [Route("[controller]")] // Establece la ruta base para todas las solicitudes que maneja este controlador.

    public class ApuestasController
    {
        [HttpPost] // Define que este método maneja solicitudes HTTP POST.
        public bool altaApuestas([FromBody] PostApuestasDTO altaApuestasBody)
        {
            // Llama al manejador para agregar una nueva apuesta utilizando los datos proporcionados en el cuerpo de la solicitud.
            return ApuestasHandler.altaApuestasHandler(altaApuestasBody);
        }

        [HttpGet] // Define que este método maneja solicitudes HTTP GET.
        public ApuestasResponse consultasApuestas([FromQuery] GetApuestasDTO consultaApuestasQuery)
        {
            // Llama al manejador para consultar las apuestas según los criterios proporcionados en la consulta de la URL.
            return ApuestasHandler.consultaApuestasHandler(consultaApuestasQuery);
        }

        [HttpPut] // Define que este método maneja solicitudes HTTP PUT.
        public bool modificacionApuestas([FromBody] PutApuestasDTO modificacionesApuestasBody)
        {
            // Llama al manejador para modificar una apuesta existente utilizando los datos proporcionados en el cuerpo de la solicitud.
            return ApuestasHandler.modificacionApuestasHandler(modificacionesApuestasBody);
        }

        [HttpDelete] // Define que este método maneja solicitudes HTTP DELETE.
        public bool bajaApuestas([FromBody] DeleteApuestasDTO bajaApuestasBody)
        {
            // Llama al manejador para eliminar una apuesta utilizando los datos proporcionados en el cuerpo de la solicitud.
            return ApuestasHandler.bajaApuestasHandler(bajaApuestasBody);
        }
    }
}
