using Dapper;
using Microsoft.Data.SqlClient;
namespace MatchRollInfo360.Models
{
    public class BD
    {
        private static string _connectionString = @"Server=localhost;DataBase=MatchAndRoll;Integrated Security=True;TrustServerCertificate=True;";

        public string BuscarSesion(string email, string contraseña)
        {
            string id = "-1";

            string query = @"
                    SELECT idUsuario
                    FROM Usuarios
                    WHERE email = @Email
                    AND contraseña = @contraseña";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string resultado = connection.QueryFirstOrDefault<string>(query, new { Email = email, contraseña = contraseña });

                if (resultado != null)
                {
                    id = resultado;
                }
            }
            return id;
        }
        public bool ValidarNombreUsuario(string apodo){
            string query = "SELECT COUNT(apodo) FROM Usuarios WHERE apodo = @apodo";
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                count = connection.QueryFirstOrDefault<int>(query, new { apodo = apodo }); 
            }
            if (count > 0)
            {
                return false;
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

        public Usuario? MostrarUsuarioCompleto(int id)
        {
            Usuario? usuario = MostrarUsuario(id);
            if (usuario == null)
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                usuario.etiquetas = connection.Query<Etiqueta>(@"
                    SELECT e.*
                    FROM Etiquetas e
                    INNER JOIN UsuarioEtiquetas ue ON e.idEtiqueta = ue.idEtiqueta
                    WHERE ue.idUsuario = @IdUsuario
                    ORDER BY e.nombreEtiqueta", new { IdUsuario = id }).ToList();

                usuario.videos = connection.Query<Video>(@"
                    SELECT *
                    FROM Videos
                    WHERE idUsuario = @IdUsuario
                    ORDER BY idVideo", new { IdUsuario = id }).ToList();
            }

            return usuario;
        }

        public bool ValidarEmail(string email)
        {
            string query = @"
                    SELECT COUNT(email)
                    FROM Usuarios
                    WHERE email = @Email";
            int count = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                count = connection.QueryFirstOrDefault<int>(query, new { Email = email });
            }
            if (count > 0)
            {
                return false;
            }
            return true;
        }

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                usuarios = connection.Query<Usuario>("SELECT * FROM Usuarios").ToList();
            }
            return usuarios;
        }

        public List<SalaEnsayo> ObtenerSalas()
        {
            List<SalaEnsayo> salas = new List<SalaEnsayo>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                salas = connection.Query<SalaEnsayo>("SELECT * FROM SalasEnsayo").ToList();
            }
            return salas;
        }

        public List<Reserva> ObtenerReservas()
        {
            List<Reserva> reservas = new List<Reserva>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                reservas = connection.Query<Reserva>("SELECT * FROM Reservas").ToList();
            }
            return reservas;
        }

        public Usuario? ObtenerUsuarioPorId(int idUsuario)
        {
            Usuario? user = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                user = connection.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuarios WHERE IdUsuario = @IdUsuario", new { IdUsuario = idUsuario });
            }
            return user;
        }
        public void CrearUsuarioNoCompleto(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO Usuarios (email, contraseña, apodo, edad, genero)
                    VALUES (@email, @contraseña, @apodo, @edad, @genero); SELECT SCOPE_IDENTITY();";

                usuario.idUsuario = connection.QueryFirstOrDefault<int>(sql, usuario);
            }
        }


        public void CrearUsuarioEtiquetas(int id, List<int> IdsEtiquetas)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var idEtiqueta in IdsEtiquetas)
                {
                    var sql = @"
                        INSERT INTO UsuarioEtiquetas (idUsuario, idEtiqueta)
                        VALUES (@id, @idEtiqueta);";

                    connection.Execute(sql, new {idEtiqueta = idEtiqueta, id = id});
                }
            }
        }
        public void ActualizarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO Usuarios (apodo, contraseña, nombre, apellido, tipoUsuario, idUbicacion, email, biografia, instrumentoPrincipal, nivelExperiencia, genero, edad)
                    VALUES (@apodo, @contraseña, @nombre, @apellido, @tipoUsuario, @idUbicacion, @email, @biografia, @instrumentoPrincipal, @nivelExperiencia, @genero, @edad);";

                connection.Execute(sql, usuario);
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
            List<ProyectoMusical> proyectos = new List<ProyectoMusical>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                proyectos = connection.Query<ProyectoMusical>("SELECT * FROM ProyectosMusicales").ToList();
            }
            return proyectos;
        }

        public List<Match> ObtenerMatches()
        {
            List<Match> matches = new List<Match>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                matches = connection.Query<Match>("SELECT * FROM Matches").ToList();
            }
            return matches;
        }

        public List<Etiqueta> DevolverEtiquetasPorIds(List<int> ids)
        {
            List<Etiqueta> etiquetas = new List<Etiqueta>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                etiquetas = connection.Query<Etiqueta>("SELECT * FROM Etiquetas WHERE idEtiqueta IN @ids", new { ids }).ToList();
            }
            return etiquetas;
        }

        public List<Etiqueta> ObtenerEtiquetasPorUsuario(int idUsuario)
        {
            List<Etiqueta> etiquetas = new List<Etiqueta>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                etiquetas = connection.Query<Etiqueta>(@"
                    SELECT e.*
                    FROM Etiquetas e
                    INNER JOIN UsuarioEtiquetas ue ON e.idEtiqueta = ue.idEtiqueta
                    WHERE ue.idUsuario = @IdUsuario
                    ORDER BY e.idEtiqueta", new { IdUsuario = idUsuario }).ToList();
            }
            return etiquetas;
        }

        public List<Etiqueta> MostrarEtiquetas()
        {
            List<Etiqueta> etiquetas = new List<Etiqueta>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                etiquetas = connection.Query<Etiqueta>("SELECT * FROM Etiquetas").ToList();
            }
            return etiquetas;
        }
    }
}
