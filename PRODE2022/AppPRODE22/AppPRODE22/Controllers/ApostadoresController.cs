using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppPRODE22.Controllers
{

    [ApiController]
    [Route("[controller]")]


    public class ApostadoresController
    {
        [HttpPost]

        public bool altaApostadores([FromBody] PostApostadoresDTO altaApostadoresBody)
        {

            return ApostadoresHandler.altaApostadoresHandler(altaApostadoresBody);

        }

        // ---------------------




        /* public List<GetApostadoresDTO> consultaApostadores([FromBody] GetApostadoresDTO consultaApostadoresBody)
         {

             return ApostadoresHandler.consultaApostadoresHandler(consultaApostadoresBody);
         }
        */

        [HttpGet]
        public ApostadoresResponse consultaApostadorse([FromQuery] GetApostadoresDTO consultaApostadoresBody)
        {
            return ApostadoresHandler.consultaApostadoresHandler(consultaApostadoresBody);
        }

        [HttpPut]

        public bool modificacionApostadores([FromBody]PutApostadoresDTO modificacionApostadoresBody)
        {

            return ApostadoresHandler.modificacionApostadoresHandler(modificacionApostadoresBody);
    
        }

        [HttpDelete]

        public bool bajaApostadores([FromBody]DeleteApostadoresDTO bajaApostadoresBody)
        {

            return ApostadoresHandler.bajaApostadoresHandler(bajaApostadoresBody);
        }

    }
}
