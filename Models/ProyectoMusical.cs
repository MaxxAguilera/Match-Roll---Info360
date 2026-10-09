namespace MatchandRoll.Models;

public class ProyectoMusical
{
    public int idProyecto { get; set; }
    public int idUsuarioCreador { get; set; }
    public string nombreProyecto { get; set; } = string.Empty;
    public string? descripcion { get; set; }
    public string? enlaceDemostracion { get; set; }
}
