using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppPRODE22.Controllers
{

    [ApiController]
    [Route("[controller]")]


    public class CompetenciaController
    {
        [HttpPost]

        public bool altaCompetencia([FromBody] PostCompetenciasDTO altaCompetenciaBody)
        {

            return CompetenciaHandler.altaCompetenciaHandler(altaCompetenciaBody);

        }
        
        // ---------------------

        [HttpGet]

        // Si se le informa al backend el ID 0, devolvera todas las competencias, de otra forma, buscara la competencia pedida.
        public List<GetCompetenciasDTO> consultaCompetencia([FromBody] GetCompetenciasDTO consultaCompetenciaBody)
        {

            return CompetenciaHandler.consultaCompetenciaHandler(consultaCompetenciaBody);
        }

        [HttpPut]

        public bool modificacionCompetencia([FromBody]PutCompetenciaDTO modificacionCompetenciaBody)
        {

            return CompetenciaHandler.modificacionCompetenciaHandler(modificacionCompetenciaBody);
    
        }

        [HttpDelete]

        public bool bajaCompetencia([FromBody]DeleteCompetenciasDTO bajaCompetenciaBody)
        {

            return CompetenciaHandler.bajaCompetenciaHandler(bajaCompetenciaBody);
        }

    }
}
