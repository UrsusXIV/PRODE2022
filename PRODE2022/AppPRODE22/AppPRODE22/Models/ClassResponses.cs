namespace AppPRODE22.Repository
{
    public class EquiposResponse
    {
        public List<Controllers.DTOs.GetEquipoDTO> Equipos { get; set; }
    }

    public class SedesResponse
    {
        public List<Controllers.DTOs.GetSedeDTO> Sede { get; set; }
    }

    public class ApostadoresResponse
    {
        public List<Controllers.DTOs.GetApostadoresDTO> Apostadores { get; set; }
    }

    public class CompetenciasResponse
    {
        public List<Controllers.DTOs.GetCompetenciasDTO> Competencias { get; set; }
    }

    public class EquiposXCompetenciaResponse
    {
        public List<Controllers.DTOs.GetEquiposXCompetenciaDTO> EquiposXCompetencia { get; set; }
    }

    public class PartidosGruposResponse
    {
        public List<Controllers.DTOs.GetPartidosGruposDTO> PartidosGrupos { get; set; }
    }

    public class GrupoApuestasResponse
    {
        public List<Controllers.DTOs.GetGruposApuestasDTO> GrupoApuestas { get; set; }
    }

    public class ApostadoresXGrupoResponse
    {
        public List<Controllers.DTOs.GetApostadoresXGrupo> ApostadoresXGrupos { get; set; }
    }

    public class ApuestasResponse
    {
        public List<Controllers.DTOs.GetApuestasDTO> Apuestas { get; set; }
    }

    public class PosicionesResponse
    {
        public List <Controllers.DTOs.GetPosiciones> Posiciones { get; set; }
    }
}