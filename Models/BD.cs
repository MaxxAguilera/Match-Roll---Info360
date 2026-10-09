using Dapper;
using Microsoft.Data.SqlClient;

namespace MatchandRoll.Models
{
    public class BD
    {
        private static string _connectionString = @"Server=localhost;DataBase=MatchAndRoll;Integrated Security=True;TrustServerCertificate=True;";

        public string BuscarSesion(string email, string contrasena)
        {
            string id = "-1";

            string query = @"
                    SELECT idUsuario
                    FROM Usuarios
                    WHERE email = @Email
                    AND contrasena = @Contrasena";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string resultado = connection.QueryFirstOrDefault<string>(query, new { Email = email, Contrasena = contrasena });

                if (resultado != null)
                {
                    id = resultado;
                }
            }
            return id;
        }
        public bool ValidarNombreUsuario(string apodo){
            string query = "SELECT COUNT(apodo) FROM Usuarios WHERE apodo = @apodo";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                int count = connection.QueryFirstOrDefault<int>(query, new { apodo = apodo });
                if (count > 0)
                {
                    return false;
                }  
            }
            return true;
        }

        public Usuario MostrarUsuario(int id)
        {
            Usuario user = null;

            string query = "SELECT * FROM Usuarios WHERE idUsuario = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                user = connection.QueryFirstOrDefault<Usuario>(query, new { Id = id });
            }

            return user;
        }

        public bool ValidarEmail(string email)
        {
            string query = @"
                    SELECT COUNT(email)
                    FROM Usuarios
                    WHERE email = @Email";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                int count = connection.QueryFirstOrDefault<int>(
                    query,
                    new { Email = email }
                );

                if (count > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public List<Usuario> ObtenerUsuarios()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Usuario>("SELECT * FROM Usuarios").ToList();
            }
        }

        public List<SalaEnsayo> ObtenerSalas()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<SalaEnsayo>("SELECT * FROM SalasEnsayo").ToList();
            }
        }

        public List<Reserva> ObtenerReservas()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Reserva>("SELECT * FROM Reservas").ToList();
            }
        }

        public Usuario? ObtenerUsuarioPorId(int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Usuario>(
                    "SELECT * FROM Usuarios WHERE IdUsuario = @IdUsuario",
                    new { IdUsuario = idUsuario }
                );
            }
        }

        public int CrearUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                            INSERT INTO Usuarios (Nombre, Apellido, Email, Contrasena, TipoUsuario, Biografia, InstrumentoPrincipal, NivelExperiencia, IdUbicacion, Genero, Apodo, Edad)
                            VALUES (@Nombre, @Apellido, @Email, @Contrasena, @TipoUsuario, @Biografia, @InstrumentoPrincipal, @NivelExperiencia, @IdUbicacion, @Genero, @Apodo, @Edad);
                            SELECT CAST(SCOPE_IDENTITY() AS int);";

                return connection.QuerySingle<int>(sql, usuario);
            }
        }

        public void CrearReserva(Reserva reserva)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                            INSERT INTO Reservas (IdSala, IdUsuario, FechaHoraInicio, FechaHoraFin, MontoTotal, EstadoReserva, ResenaComentario, PuntajeResena)
                            VALUES (@IdSala, @IdUsuario, @FechaHoraInicio, @FechaHoraFin, @MontoTotal, @EstadoReserva, @ResenaComentario, @PuntajeResena);";

                connection.Execute(sql, reserva);
            }
        }

        public List<ProyectoMusical> ObtenerProyectos()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<ProyectoMusical>("SELECT * FROM ProyectosMusicales").ToList();
            }
        }

        public List<Match> ObtenerMatches()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Match>("SELECT * FROM Matches").ToList();
            }
        }

        public List<Etiqueta> MostrarEtiquetas()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Etiqueta>("SELECT * FROM Etiquetas").ToList();
            }
        }
    }
}
