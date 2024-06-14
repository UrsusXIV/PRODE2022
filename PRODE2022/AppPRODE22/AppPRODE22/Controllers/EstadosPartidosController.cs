using AppPRODE22.Controllers.DTOs; // Importa los Data Transfer Objects (DTOs) necesarios para la transferencia de datos entre el frontend y el backend.
using AppPRODE22.Repository; // Importa el manejador de datos que realiza operaciones de base de datos relacionadas con los estados de partidos.
using Microsoft.AspNetCore.Mvc; // Importa los componentes de ASP.NET Core necesarios para la construcción de la API.

namespace AppPRODE22.Controllers // Define el espacio de nombres del controlador de la API.
{
    [ApiController] // Indica que esta clase es un controlador de API.
    [Route("[controller]")] // Define la ruta base para todas las solicitudes manejadas por este controlador.

    public class EstadosPartidosController
    {
        [HttpPost] // Define que este método maneja solicitudes HTTP POST.
        public bool altaEstado([FromBody] PostEstadosPartidosDTO altaEstadoBody)
        {
            // Llama al manejador para agregar un nuevo estado de partido utilizando los datos proporcionados en el cuerpo de la solicitud.
            return EstadosPartidosHandler.altaEstadoHandler(altaEstadoBody);
        }

        [HttpGet] // Define que este método maneja solicitudes HTTP GET.
        // Si se le informa al backend el ID 0, devolvera todas los estados, de otra forma, buscara los estados pedido.
        public List<GetEstadosPartidosDTO> consultaEstado([FromBody] GetEstadosPartidosDTO consultaEstadoBody)
        {
            // Llama al manejador para consultar estados de partido según los criterios proporcionados en el cuerpo de la solicitud.
            return EstadosPartidosHandler.consultaEstadoHandler(consultaEstadoBody);
        }
    }
}
