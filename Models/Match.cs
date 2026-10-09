namespace MatchandRoll.Models;

public class Match
{
    public int idMatch { get; set; }
    public int idUsuarioSolicitante { get; set; }
    public int idUsuarioReceptor { get; set; }
    public DateTime fechaMatch { get; set; }
    public string estadoMatch { get; set; } = "Pendiente";
}
