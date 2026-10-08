using Dapper;
using Microsoft.Data.SqlClient;

namespace MatchandRoll.Models;

public class BD
{
    private readonly string _connectionString = @"Server=localhost;Database=MatchAndRoll;Integrated Security=True;TrustServerCertificate=True;";

    public List<Usuario> ObtenerUsuarios()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Usuario>("SELECT * FROM Usuarios").ToList();
    }

    public List<SalaEnsayo> ObtenerSalas()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<SalaEnsayo>("SELECT * FROM SalasEnsayo").ToList();
    }

    public List<Reserva> ObtenerReservas()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Reserva>("SELECT * FROM Reservas").ToList();
    }

    public Usuario? ObtenerUsuarioPorId(int idUsuario)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QueryFirstOrDefault<Usuario>(
            "SELECT * FROM Usuarios WHERE IdUsuario = @IdUsuario",
            new { IdUsuario = idUsuario });
    }

    public int CrearUsuario(Usuario usuario)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"
            INSERT INTO Usuarios (Nombre, Apellido, Email, Contrasena, TipoUsuario, Biografia, InstrumentoPrincipal, NivelExperiencia, IdUbicacion)
            VALUES (@Nombre, @Apellido, @Email, @Contrasena, @TipoUsuario, @Biografia, @InstrumentoPrincipal, @NivelExperiencia, @IdUbicacion);
            SELECT CAST(SCOPE_IDENTITY() AS int);";

        return connection.QuerySingle<int>(sql, usuario);
    }

    public void CrearReserva(Reserva reserva)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"
            INSERT INTO Reservas (IdSala, IdUsuario, FechaHoraInicio, FechaHoraFin, MontoTotal, EstadoReserva, ResenaComentario, PuntajeResena)
            VALUES (@IdSala, @IdUsuario, @FechaHoraInicio, @FechaHoraFin, @MontoTotal, @EstadoReserva, @ResenaComentario, @PuntajeResena);";

        connection.Execute(sql, reserva);
    }

    public List<ProyectoMusical> ObtenerProyectos()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<ProyectoMusical>("SELECT * FROM ProyectosMusicales").ToList();
    }

    public List<Match> ObtenerMatches()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Match>("SELECT * FROM Matches").ToList();
    }
}
