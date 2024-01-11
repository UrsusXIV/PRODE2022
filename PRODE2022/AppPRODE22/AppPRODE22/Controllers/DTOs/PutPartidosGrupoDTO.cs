namespace AppPRODE22.Controllers.DTOs
{
    public class PutPartidosGruposDTO
    {
        public int PartIDPartido { get; set; } = 0;

        public int PartIDCompetencia { get; set; } = 0;

        public string PartGrupo { get; set; } = string.Empty;

        public int PartIDEquipoL { get; set; } = 0;

        public int PartIDEquipoV { get; set; } = 0;

        public int PartIDSede { get; set; } = 0;

        public string PartFechaDate { get; set; } = string.Empty;

        public string PartHoraTime { get; set; } = string.Empty;

        public int PartIDEstado { get; set; } = 0;

        public int PartGolesL { get; set; } = 0;

        public int PartGolesV { get; set; } = 0;

        public int PartPuntosL { get; set; } = 0;

        public int PartPuntosV { get; set; } = 0;



    }
}
