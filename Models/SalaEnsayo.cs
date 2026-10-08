namespace MatchandRoll.Models;

public class SalaEnsayo
{
    public int IdSala { get; set; }
    public int IdUsuarioDueno { get; set; }
    public int IdUbicacion { get; set; }
    public string NombreSala { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public decimal PrecioPorHora { get; set; }
    public string? Descripcion { get; set; }
    public string? Equipamiento { get; set; }
    public TimeSpan? HorarioApertura { get; set; }
    public TimeSpan? HorarioCierre { get; set; }
    public int? DuracionTurnoHoras { get; set; }
    public decimal? ResenaPromedio { get; set; }
}
