namespace MatchandRoll.Models;

public class SalaEnsayo
{
    public int idSala { get; set; }
    public int idUsuarioDueno { get; set; }
    public int idUbicacion { get; set; }
    public string nombreSala { get; set; } = string.Empty;
    public string direccion { get; set; } = string.Empty;
    public decimal precioPorHora { get; set; }
    public string? descripcion { get; set; }
    public string? equipamiento { get; set; }
    public TimeSpan? horarioApertura { get; set; }
    public TimeSpan? horarioCierre { get; set; }
    public int? duracionTurnoHoras { get; set; }
    public decimal? resenaPromedio { get; set; }
}
