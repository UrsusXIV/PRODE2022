using AppPRODE22.Controllers.DTOs; // Importa los Data Transfer Objects (DTOs) necesarios para la transferencia de datos.
using AppPRODE22.Repository; // Importa el repositorio que maneja las operaciones relacionadas con las sedes.
using Microsoft.AspNetCore.Mvc; // Importa los componentes de ASP.NET Core necesarios para la construcción de la API.

namespace AppPRODE22.Controllers // Define el espacio de nombres del controlador de la API.
{
    [ApiController] // Indica que esta clase es un controlador de API.
    [Route("[controller]")] // Establece la ruta base para todas las solicitudes manejadas por este controlador

    public class SedeController
    {
        [HttpPost] // Define que este método maneja solicitudes HTTP POST.
        public bool altaSede([FromBody] PostSedeDTO altaSedeBody)
        {
            // Llama al manejador para agregar una nueva sede utilizando los datos proporcionados en el cuerpo de la solicitud.
            return SedeHandler.altaSedeHandler(altaSedeBody);
        }

        [HttpGet] // Define que este método maneja solicitudes HTTP GET.
        // Si se le informa al backend el ID 0, devolvera todas las sedes, de otra forma, buscara la sede pedida.
        public SedesResponse consultaSede([FromQuery] GetSedeDTO consultaSedeBody)
        {
            // Llama al manejador para consultar sedes según los parámetros proporcionados en la consulta de la URL.
            return SedeHandler.consultaSedeHandler(consultaSedeBody);
        }

        [HttpPut] // Define que este método maneja solicitudes HTTP PUT.
        public bool modificacionSede([FromBody] PutSedeDTO modificacionSedeBody)
        {
            // Llama al manejador para modificar una sede utilizando los datos proporcionados en el cuerpo de la solicitud.
            return SedeHandler.modificacionSedeHandler(modificacionSedeBody);
        }

        [HttpDelete] // Define que este método maneja solicitudes HTTP DELETE.
        public bool bajaSede([FromBody] DeleteSedeDTO bajaSedeBody)
        {
            // Llama al manejador para eliminar una sede utilizando los datos proporcionados en el cuerpo de la solicitud.
            return SedeHandler.bajaSedeHandler(bajaSedeBody);
        }
    }
}
