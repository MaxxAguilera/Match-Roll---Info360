namespace MatchRollInfo360.Models;

public class Chat
{
    public int idChat { get; set; }
    public int idOtroUsuario { get; set; }
    public string apodo { get; set; } = string.Empty;
    public string ultimoMensaje { get; set; } = string.Empty;
    public DateTime fechaUltimoMensaje { get; set; }
}
