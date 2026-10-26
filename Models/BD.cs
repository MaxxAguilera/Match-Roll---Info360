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
                string? resultado = connection.QueryFirstOrDefault<string>(query, new { Email = email, contraseña = contraseña });

                if (resultado != null)
                {
                    id = resultado;
                }
            }
            return id;
        }

        public bool ValidarNombreUsuario(string apodo)
        {
            string query = @"
                SELECT COUNT(apodo)
                FROM Usuarios
                WHERE apodo = @apodo";

            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                count = connection.QueryFirstOrDefault<int>(query, new { apodo = apodo});
            }

            if (count > 0)
            {
                return false; 
            }
            else
            {
                return true; 
            }
        }
        public bool ValidarCambiodeNombreUsuario(string apodo)
        {
            string query = @"
                SELECT COUNT(apodo)
                FROM Usuarios
                WHERE apodo = @apodo";

            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                count = connection.Query<int>(query, new { apodo = apodo}).ToList().Count;
            }

            if (count > 1)
            {
                return false; 
            }
            else
            {
                return true; 
            }
        }

        public bool ValidarCambiodeEmail(string email)
        {
            string query = @"
                SELECT COUNT(email)
                FROM Usuarios
                WHERE email = @email";

            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                count = connection.Query<int>(query, new { email = email}).ToList().Count;
            }

            if (count > 1)
            {
                return false; 
            }
            else
            {
                return true; 
            }
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
            else
            {
                return true; 
            }
        }

        public void ActualizarUsuarioEtiquetas(int idUsuario, List<int> IdsEtiquetas)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var ids = (IdsEtiquetas ?? new List<int>())
                    .Where(x => x > 0)
                    .Distinct()
                    .ToList();

                connection.Execute("DELETE FROM UsuarioEtiquetas WHERE idUsuario = @idUsuario", new { idUsuario = idUsuario });

                if (!ids.Any())
                {
                    AsegurarEtiquetaAleatoria(idUsuario);
                    return;
                }

                foreach (var idEtiqueta in ids)
                {
                    connection.Execute(@"
                        IF NOT EXISTS (
                            SELECT 1
                            FROM UsuarioEtiquetas
                            WHERE idUsuario = @idUsuario
                              AND idEtiqueta = @idEtiqueta)
                        BEGIN
                            INSERT INTO UsuarioEtiquetas (idUsuario, idEtiqueta)
                            VALUES (@idUsuario, @idEtiqueta)
                        END;",
                        new { idUsuario = idUsuario, idEtiqueta = idEtiqueta });
                }
            }
        }
        public void ActualizarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    UPDATE Usuarios
                    SET apodo = @apodo,
                        contraseña = @contraseña,
                        nombre = @nombre,
                        apellido = @apellido,
                        tipoUsuario = @tipoUsuario,
                        idUbicacion = @idUbicacion,
                        email = @email,
                        biografia = @biografia,
                        instrumentoPrincipal = @instrumentoPrincipal,
                        nivelExperiencia = @nivelExperiencia,
                        genero = @genero,
                        edad = @edad,
                        videoPrincipal = @videoPrincipal
                    WHERE idUsuario = @idUsuario;";

                connection.Execute(sql, new
                {
                    usuario.idUsuario,
                    usuario.apodo,
                    usuario.contraseña,
                    usuario.nombre,
                    usuario.apellido,
                    usuario.tipoUsuario,
                    usuario.idUbicacion,
                    usuario.email,
                    usuario.biografia,
                    usuario.instrumentoPrincipal,
                    usuario.nivelExperiencia,
                    usuario.genero,
                    usuario.edad,
                    usuario.videoPrincipal});
            }
        }
        public void GuardarVideoDesdeUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UPDATE Usuarios SET videoPrincipal = @videoPrincipal WHERE idUsuario = @idUsuario",
                    new { idUsuario = usuario.idUsuario, videoPrincipal = usuario.videoPrincipal.Trim() });
            }
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
                            VALUES (@idUsuario, @idEtiqueta)
                        END;";

                    connection.Execute(sql, new { idUsuario = id, idEtiqueta = idEtiqueta });
                }
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

        public Usuario MostrarUsuario(int id)
        {
            Usuario user = new Usuario();

            string query = "SELECT * FROM Usuarios WHERE idUsuario = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                user = connection.QueryFirstOrDefault<Usuario>(query, new { Id = id }) ?? new Usuario();
            }

            return user;
        }
        public void AsegurarEtiquetaAleatoria(int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                int cantidad = connection.QuerySingle<int>(
                    "SELECT COUNT(*) FROM UsuarioEtiquetas WHERE idUsuario = @idUsuario",
                    new { idUsuario });

                if (cantidad > 0)
                {
                    return;
                }

                var ids = connection.Query<int>("SELECT idEtiqueta FROM Etiquetas").ToList();
                if (!ids.Any())
                {
                    return;
                }

                int idEtiqueta = ids.OrderBy(_ => Guid.NewGuid()).First();

                connection.Execute(
                    "INSERT INTO UsuarioEtiquetas (idUsuario, idEtiqueta) VALUES (@idUsuario, @idEtiqueta)",
                    new { idUsuario, idEtiqueta });
            }
        }



        public List<int> ObtenerUbicacionesDisponibles()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<int>("SELECT idUbicacion FROM Ubicaciones").ToList();
            }
        }

        public List<int> ObtenerIdsEtiquetasPorUsuario(int idUsuario)
        {
            List<int> idsEtiquetas = new List<int>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                idsEtiquetas = connection.Query<int>("SELECT idEtiqueta FROM UsuarioEtiquetas WHERE idUsuario = @idUsuario", new { idUsuario }).ToList();
            }
            return idsEtiquetas;
        }

        public List<Usuario> ObtenerEtiquetasPorUsuario(int idUsuario)
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                usuarios = connection.Query<Usuario>(@"
                    SELECT u.idUsuario, group by 
                    FROM Usuarios u
                    INNER JOIN UsuarioEtiquetas ue ON u.idUsuario = ue.idUsuario
                    WHERE ue.idUsuario = @idUsuario", new { idUsuario }).ToList();
            }
            return usuarios;
        }
    }
}
