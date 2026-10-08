namespace MatchandRoll.Models;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public string TipoUsuario { get; set; } = "Músico";
    public string? Biografia { get; set; }
    public string? InstrumentoPrincipal { get; set; }
    public string? NivelExperiencia { get; set; }
    public int? IdUbicacion { get; set; }
}
