using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppPRODE22.Controllers
{

    [ApiController]
    [Route("[controller]")]


    public class PartidosGrupoController
    {
        [HttpPost]

        public bool altaPartidosGrupo([FromBody] PostPartidosGruposDTO altaPartidosGrupoBody)
        {

            return PartidosGrupoHandler.altaPartidosGrupoHandler(altaPartidosGrupoBody);

        }
        
        // ---------------------

        [HttpGet]

        // 
        public List<GetPartidosGruposDTO> consultaPartidosGrupo([FromBody] GetPartidosGruposDTO consultaPartidosGrupoBody)
        {

            return PartidosGrupoHandler.consultaPartidosGrupoHandler(consultaPartidosGrupoBody);
        }

        [HttpPut]

        public bool modificacionPartidosGrupo([FromBody]PutPartidosGruposDTO modificacionPartidosGrupoBody)
        {

            return PartidosGrupoHandler.modificacionPartidosGrupoHandler(modificacionPartidosGrupoBody);
    
        }

        [HttpDelete]

        public bool bajaPartidosGrupo([FromBody]DeletePartidosGruposDTO bajaPartidosGrupoBody)
        {

            return PartidosGrupoHandler.bajaPartidosGrupoHandler(bajaPartidosGrupoBody);
        }

    }
}
