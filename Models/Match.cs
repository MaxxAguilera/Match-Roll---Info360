namespace MatchandRoll.Models;

public class Match
{
    public int IdMatch { get; set; }
    public int IdUsuarioSolicitante { get; set; }
    public int IdUsuarioReceptor { get; set; }
    public DateTime FechaMatch { get; set; }
    public string EstadoMatch { get; set; } = "Pendiente";
}
