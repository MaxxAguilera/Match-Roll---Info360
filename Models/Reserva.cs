namespace MatchandRoll.Models;

public class Reserva
{
    public int IdReserva { get; set; }
    public int IdSala { get; set; }
    public int IdUsuario { get; set; }
    public DateTime FechaHoraInicio { get; set; }
    public DateTime FechaHoraFin { get; set; }
    public decimal MontoTotal { get; set; }
    public string EstadoReserva { get; set; } = "Pendiente";
    public string? ResenaComentario { get; set; }
    public int? PuntajeResena { get; set; }
}
