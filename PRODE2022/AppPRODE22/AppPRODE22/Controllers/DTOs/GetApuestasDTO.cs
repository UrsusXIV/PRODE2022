namespace AppPRODE22.Controllers.DTOs
{
    public class GetApuestasDTO
    {
        public int ApIDPartido { get; set; }

        public int? ApIDCompetencia { get; set; } = null;

        public int? ApIDApostador { get; set; } = null;

        public int ApIDEquipoL { get; set; }

        public int ApIDEquipoV { get; set; }
     
        public string EquipoLNombre { get; set; } = "";

        public string EquipoVNombre { get; set; } = "";

        public int ApGolesL { get; set; } = 0;

        public int ApGolesV { get; set; } = 0;

        public int ApPuntosObtenidos { get; set; } = 0;
    }
}
