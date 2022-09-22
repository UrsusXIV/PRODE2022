using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppPRODE22.Controllers
{

    [ApiController]
    [Route("[controller]")]


    public class SedeController
    {
        [HttpPost]

        public bool altaSede([FromBody] PostSedeDTO altaSedeBody)
        {

            return SedeHandler.altaSedeHandler(altaSedeBody);

        }
        
        // ---------------------

        [HttpGet]

        // Si se le informa al backend el ID 0, devolvera todas las sedes, de otra forma, buscara el equipo pedido.
        public List<GetSedeDTO> consultaSede([FromBody] GetSedeDTO consultaSedeBody)
        {

            return SedeHandler.consultaSedeHandler(consultaSedeBody);
        }

        [HttpPut]

        public bool modificacionSede([FromBody]PutSedeDTO modificacionSedeBody)
        {

            return SedeHandler.modificacionSedeHandler(modificacionSedeBody);
    
        }

        [HttpDelete]

        public bool bajaSede([FromBody]DeleteSedeDTO bajaSedeBody)
        {

            return SedeHandler.bajaSedeHandler(bajaSedeBody);
        }

    }
}
