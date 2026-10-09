namespace MatchandRoll.Models;

public class Reserva
{
    public int idReserva { get; set; }
    public int idSala { get; set; }
    public int idUsuario { get; set; }
    public DateTime fechaHoraInicio { get; set; }
    public DateTime fechaHoraFin { get; set; }
    public double montoTotal { get; set; }
    public string estadoReserva { get; set; } = "Pendiente";
    public string? resenaComentario { get; set; }
    public int? puntajeResena { get; set; }
}
