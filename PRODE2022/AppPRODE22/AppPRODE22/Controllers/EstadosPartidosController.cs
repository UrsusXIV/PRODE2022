﻿using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppPRODE22.Controllers
{

    [ApiController]
    [Route("[controller]")]


    public class EstadosPartidosController
    {
        [HttpPost]

        public bool altaEstado([FromBody] PostEstadosPartidosDTO altaEstadoBody)
        {

            return EstadosPartidosHandler.altaEstadoHandler(altaEstadoBody);

        }
        
        // ---------------------

        [HttpGet]

        // Si se le informa al backend el ID 0, devolvera todas las sedes, de otra forma, buscara el equipo pedido.
        public List<GetEstadosPartidosDTO> consultaEstado([FromBody] GetEstadosPartidosDTO consultaEstadoBody)
        {

            return EstadosPartidosHandler.consultaEstadoHandler(consultaEstadoBody);
        }

    }
}
