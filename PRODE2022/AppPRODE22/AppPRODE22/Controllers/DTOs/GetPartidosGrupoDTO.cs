﻿namespace AppPRODE22.Controllers.DTOs
{
    public class GetPartidosGruposDTO

    {
        public int PartIDPartido { get; set; }

        public int PartIDCompetencia { get; set; }

        public string PartGrupo { get; set; } = "";

        public int PartIDEquipoL { get; set; }

        public int PartIDEquipoV { get; set; }

        public int PartIDSede { get; set; }

        public string PartFechaDate { get; set; } = "";

        public string PartHoraTime { get; set; } = "";

        public int PartIDEstado { get; set; }

        public int PartGolesL { get; set; }

        public int PartGolesV { get; set; }

        public int PartPuntosL { get; set; }

        public int PartPuntosV { get; set; }

        public string EquipoLNombre { get; set; } = "";

        public string EquipoVNombre { get; set; } = "";

    }
}
