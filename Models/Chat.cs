namespace MatchRollInfo360.Models;

public class Chat
{
    public int idChat { get; set; }
    public int idUsuarioEmisor { get; set; }
    public int idUsuarioReceptor { get; set; }
    public string ultimoMensaje { get; set; } = string.Empty;
    public DateTime fechaUltimoMensaje { get; set; }
}
