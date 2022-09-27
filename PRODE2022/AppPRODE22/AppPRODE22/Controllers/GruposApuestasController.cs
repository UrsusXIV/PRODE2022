using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppPRODE22.Controllers
{

    [ApiController]
    [Route("[controller]")]


    public class GruposApuestasController
    {
        [HttpPost]

        public bool altaGruposApuestas([FromBody] PostGruposApuestasDTO altaGruposApuestasBody)
        {

            return GruposApuestasHandler.altaGruposApuestasHandler(altaGruposApuestasBody);

        }
        
        // ---------------------

        [HttpGet]        
        public List<GetGruposApuestasDTO> consultaGruposApuestas([FromBody] GetGruposApuestasDTO consultaGruposApuestasBody)
        {

            return GruposApuestasHandler.consultaGruposApuestasHandler(consultaGruposApuestasBody);
        }

        [HttpPut]

        public bool modificacionGruposApuestas([FromBody]PutGruposApuestasDTO modificacionGruposApuestasBody)
        {

            return GruposApuestasHandler.modificacionGruposApuestasHandler(modificacionGruposApuestasBody);
    
        }

        [HttpDelete]

        public bool bajaGruposApuestas([FromBody]DeleteGruposApuestasDTO bajaGruposApuestasBody)
        {

            return GruposApuestasHandler.bajaGruposApuestasHandler(bajaGruposApuestasBody);
        }

    }
}
