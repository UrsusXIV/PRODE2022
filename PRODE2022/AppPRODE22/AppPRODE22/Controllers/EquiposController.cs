using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppPRODE22.Controllers
{

    [ApiController]
    [Route("[controller]")]


    public class EquiposController
    {
        [HttpPost]

        public bool altaEquipo([FromBody] PostEquipoDTO altaEquiposBody)
        {

            return EquiposHandler.altaEquipoHandler(altaEquiposBody);

        }
        
        // ---------------------

        [HttpGet]

        // Si se le informa al backend el ID 0, devolvera todos los equipos, de otra forma, buscara el equipo pedido.
        public List<GetEquipoDTO> consultaEquipos([FromBody] GetEquipoDTO consultaEquiposBody)
        {

            return EquiposHandler.consultaEquiposHandler(consultaEquiposBody);
        }

        [HttpPut]

        public bool modificacionEquipos([FromBody]PutEquipoDTO modificacionEquipoBody)
        {

            return EquiposHandler.modificacionEquiposHandler(modificacionEquipoBody);
    
        }

        [HttpDelete]

        public bool bajaEquipos([FromBody]DeleteEquipoDTO bajaEquipoBody)
        {

            return EquiposHandler.bajaEquipoHandler(bajaEquipoBody);
        }

    }
}
