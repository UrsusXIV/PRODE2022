using AppPRODE22.Controllers.DTOs; // Importa los DTOs necesarios para las operaciones de la API.
using AppPRODE22.Repository; // Importa el manejador de datos para realizar operaciones en la base de datos.
using Microsoft.AspNetCore.Mvc; // Importa los componentes de ASP.NET Core para la construcción de la API.

namespace AppPRODE22.Controllers // Define el espacio de nombres del controlador.
{
    [ApiController] // Indica que esta clase es un controlador de API.
    [Route("[controller]")] // Define la ruta base para las acciones de este controlador.

    public class ApostadoresXGrupoController
    {
        [HttpPost] // Define que este método responde a solicitudes HTTP POST.
        public bool altaApostadoresXGrupo([FromBody] PostApostadoresXGrupo altaApostadoresXGrupoBody)
        {
            // Llama al manejador para agregar un nuevo apostador al grupo con la información proporcionada.
            return ApostadoresXGrupoHandler.altaApostadoresXGrupoHandler(altaApostadoresXGrupoBody);
        }

        [HttpGet] // Define que este método responde a solicitudes HTTP GET.
        public ApostadoresXGrupoResponse consultaApostadoresXGrupo([FromQuery] GetApostadoresXGrupo consultaApostadoresXGrupoQuery)
        {
            // Llama al manejador para consultar los datos de los apostadores en el grupo según los criterios proporcionados.
            return ApostadoresXGrupoHandler.consultaApostadoresXGrupoHandler(consultaApostadoresXGrupoQuery);
        }

        [HttpDelete] // Define que este método responde a solicitudes HTTP DELETE.
        public bool bajaApostadoresXGrupo([FromBody] DeleteApostadoresXGrupo bajaApostadoresXGrupoBody)
        {
            // Llama al manejador para eliminar un apostador del grupo según los datos proporcionados.
            return ApostadoresXGrupoHandler.bajaApostadoresXGrupoHandler(bajaApostadoresXGrupoBody);
        }
    }
}
