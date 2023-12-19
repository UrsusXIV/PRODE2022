using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppPRODE22.Controllers
{

    [ApiController]
    [Route("[controller]")]


    public class ApostadoresXGrupoController
    {
        [HttpPost]

        public bool altaApostadoresXGrupo([FromBody] PostApostadoresXGrupo altaApostadoresXGrupoBody)
        {

            return ApostadoresXGrupoHandler.altaApostadoresXGrupoHandler(altaApostadoresXGrupoBody);

        }

        // ---------------------

        /*
        [HttpGet]

        // Si se le informa al backend el ID 0, devolvera todas las sedes, de otra forma, buscara la sede pedida.
        public List<GetApostadoresXGrupo> consultaApostadoresXGrupo([FromBody] GetApostadoresXGrupo consultaApostadoresXGrupoBody)
        {

            return ApostadoresXGrupoHandler.consultaApostadoresXGrupoHandler(consultaApostadoresXGrupoBody);
        }
        */

        [HttpGet]

        public ApostadoresXGrupoResponse consultaApostadoresXGrupo([FromQuery] GetApostadoresXGrupo consultaApostadoresXGrupoQuery)
        {
            return ApostadoresXGrupoHandler.consultaApostadoresXGrupoHandler(consultaApostadoresXGrupoQuery);
        }

        [HttpDelete]

        public bool bajaApostadoresXGrupo([FromBody]DeleteApostadoresXGrupo bajaApostadoresXGrupoBody)
        {

            return ApostadoresXGrupoHandler.bajaApostadoresXGrupoHandler(bajaApostadoresXGrupoBody);
        }

    }
}
