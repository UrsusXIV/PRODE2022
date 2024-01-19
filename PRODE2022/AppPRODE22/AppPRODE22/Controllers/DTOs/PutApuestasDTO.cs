namespace AppPRODE22.Controllers.DTOs
{
    public class PutApuestasDTO
    {
        public int ApIDPartido { get; set; } = 0;

        public int ApIDCompetencia { get; set; } = 0;

        public int ApIDApostador { get; set; }

        public int ApGolesL { get; set; } = 0;

        public int ApGolesV { get; set; } = 0;

        public int ApPuntosObtenidos { get; set; } = 0;
    }
}
