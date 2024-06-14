using AppPRODE22.Controllers.DTOs; // Importa los DTOs (Data Transfer Objects) necesarios para las operaciones de la API.
using AppPRODE22.Repository; // Importa el manejador de datos para realizar operaciones en la base de datos.
using Microsoft.AspNetCore.Mvc; // Importa los componentes necesarios para construir una API usando ASP.NET Core.

namespace AppPRODE22.Controllers // Define el espacio de nombres del controlador.
{
    [ApiController] // Indica que esta clase es un controlador de API.
    [Route("[controller]")] // Define la ruta de acceso base para este controlador.

    public class ApostadoresController
    {
        [HttpPost] // Define que este método responderá a peticiones HTTP POST.
        public bool altaApostadores([FromBody] PostApostadoresDTO altaApostadoresBody)
        {
            // Llama al manejador para agregar un nuevo apostador con la información proporcionada.
            return ApostadoresHandler.altaApostadoresHandler(altaApostadoresBody);
        }

        [HttpGet] // Define que este método responderá a peticiones HTTP GET.
        public ApostadoresResponse consultaApostadorse([FromQuery] GetApostadoresDTO consultaApostadoresBody)
        {
            // Llama al manejador para consultar los datos de los apostadores según los criterios proporcionados.
            return ApostadoresHandler.consultaApostadoresHandler(consultaApostadoresBody);
        }

        [HttpPut] // Define que este método responderá a peticiones HTTP PUT.
        public bool modificacionApostadores([FromBody] PutApostadoresDTO modificacionApostadoresBody)
        {
            // Llama al manejador para modificar la información de un apostador existente.
            return ApostadoresHandler.modificacionApostadoresHandler(modificacionApostadoresBody);
        }

        [HttpDelete] // Define que este método responderá a peticiones HTTP DELETE.
        public bool bajaApostadores([FromBody] DeleteApostadoresDTO bajaApostadoresBody)
        {
            // Llama al manejador para eliminar un apostador según los datos proporcionados.
            return ApostadoresHandler.bajaApostadoresHandler(bajaApostadoresBody);
        }
    }
}
