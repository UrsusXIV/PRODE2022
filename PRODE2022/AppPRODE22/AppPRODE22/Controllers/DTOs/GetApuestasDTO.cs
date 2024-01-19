namespace AppPRODE22.Controllers.DTOs
{
    public class GetApuestasDTO
    {
        public int PartIDPartido { get; set; }

        public int PartIDCompetencia { get; set; }

        public int PartIDEquipoL { get; set; }

        public int PartIDEquipoV { get; set; }

        public int PartIDEstado { get; set; } = 0;
        
        public string EquipoLNombre { get; set; } = "";

        public string EquipoVNombre { get; set; } = "";

        public int ApGolesL { get; set; } = 0;

        public int ApGolesV { get; set; } = 0;

        public int ApPuntosObtenidos { get; set; } = 0;
    }
}
