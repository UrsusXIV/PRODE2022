using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppPRODE22.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ApuestasController
    {
        [HttpPost]

        public bool altaApuestas([FromBody] PostApuestasDTO altaApuestasBody)
        {
            return ApuestasHandler.altaApuestasHandler(altaApuestasBody);
        }

        [HttpGet]

        public ApuestasResponse consultasApuestas([FromQuery] GetApuestasDTO consultaApuestasQuery)
        {
            return ApuestasHandler.consultaApuestasHandler(consultaApuestasQuery);
        }

        /*
        [HttpPut]
        
        public bool modificacionApuestas([FromBody] PutApuestasDTO modificacionesApuestasBody)
        {
            return ApuestasHandler.modificacionApuestasHandler(modificacionesApuestasBody)
        }

        [HttpDelete]

        public bool bajaApuestas([FromBody]DeleteApuestasDTO bajaApuestasBody)
        {
            return ApuestasHandler.bajaApuestasHandler(bajaApuestasBody)
        }
        */
    }
}
