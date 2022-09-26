using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppPRODE22.Controllers
{

    [ApiController]
    [Route("[controller]")]


    public class EquiposXCompetenciaController
    {
        [HttpPost]

        // Da de alta los equipos por competencia.

        public bool altaEqpsXComp([FromBody] PostEquiposXCompetenciaDTO altaEqpsXCompBody)
        {

            return EquiposXCompetenciaHandler.altaEqpsXCompHandler(altaEqpsXCompBody);

        }
        
        

        [HttpGet]

        // Si se le informa al backend el ID 0, devolvera todas las sedes, de otra forma, buscara la sede pedida.
        public List<GetEquiposXCompetenciaDTO> consultaEqpsXComp([FromBody] GetEquiposXCompetenciaDTO consultaEqpsXCompBody)
        {

            return EquiposXCompetenciaHandler.consultaEqpsXCompHandler(consultaEqpsXCompBody);
        }

        [HttpPut]

        public bool modificacionEqpsXComp([FromBody]PutEquiposXCompetenciaDTO modificacionEqpsXCompBody)
        {

            return EquiposXCompetenciaHandler.modificacionEqpsXCompHandler(modificacionEqpsXCompBody);
    
        }

        [HttpDelete]

        public bool bajaEqpsXComp([FromBody]DeleteEquiposXCompetenciaDTO bajaEqpsXCompBody)
        {

            return EquiposXCompetenciaHandler.bajaEqpsXCompHandler(bajaEqpsXCompBody);
        }

    }
}
