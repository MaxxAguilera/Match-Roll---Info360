namespace MatchandRoll.Models;

public class ProyectoMusical
{
    public int IdProyecto { get; set; }
    public int IdUsuarioCreador { get; set; }
    public string NombreProyecto { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string? EnlaceDemostracion { get; set; }
}
