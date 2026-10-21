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

        public bool ValidarNombreUsuario(string apodo){
            return ValidarNombreUsuario(apodo, 0);
        }

        public bool ValidarNombreUsuario(string apodo, int idUsuario)
        {
            string query = @"
                SELECT COUNT(apodo)
                FROM Usuarios
                WHERE apodo = @apodo
                  AND idUsuario <> @idUsuario";

            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                count = connection.QueryFirstOrDefault<int>(query, new { apodo = apodo, idUsuario = idUsuario });
            }

            return count == 0;
        }

        public bool ValidarEmail(string email){
            return ValidarEmail(email, 0);
        }

        public bool ValidarEmail(string email, int idUsuario)
        {
            string query = @"
                    SELECT COUNT(email)
                    FROM Usuarios
                    WHERE email = @Email
                      AND idUsuario <> @idUsuario";
            int count = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                count = connection.QueryFirstOrDefault<int>(query, new { Email = email, idUsuario = idUsuario });
            }

            return count == 0;
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
                        urlVideo = @urlVideo
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
                    urlVideo = usuario.urlVideo ?? string.Empty
                });
            }
        }

        public void ActualizarVideosUsuario(int idUsuario, List<Video> videos)
        {
            if (videos == null || !videos.Any())
            {
                return;
            }

            var urlPrincipal = videos
                .Where(v => v != null && !string.IsNullOrWhiteSpace(v.urlVideo))
                .Select(v => v.urlVideo.Trim())
                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(urlPrincipal))
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(
                        "UPDATE Usuarios SET urlVideo = @urlVideo WHERE idUsuario = @idUsuario",
                        new { idUsuario, urlVideo = urlPrincipal });
                }
            }
        }

        public void GuardarVideoDesdeUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.urlVideo))
            {
                return;
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UPDATE Usuarios SET urlVideo = @urlVideo WHERE idUsuario = @idUsuario",
                    new { idUsuario = usuario.idUsuario, urlVideo = usuario.urlVideo.Trim() });
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
            var ids = (IdsEtiquetas ?? new List<int>())
                .Where(x => x > 0)
                .Distinct()
                .ToList();

            if (!ids.Any())
            {
                ids = ObtenerEtiquetasAleatorias(1);
            }

            if (!ids.Any())
            {
                return;
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var idEtiqueta in ids)
                {
                    var sql = @"
                        IF NOT EXISTS (
                            SELECT 1
                            FROM UsuarioEtiquetas
                            WHERE idUsuario = @idUsuario
                              AND idEtiqueta = @idEtiqueta)
                        BEGIN
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
            Usuario user = null!;

            string query = "SELECT * FROM Usuarios WHERE idUsuario = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                user = connection.QueryFirstOrDefault<Usuario>(query, new { Id = id }) ?? new Usuario();
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
                var tieneEtiquetas = connection.QuerySingleOrDefault<int>(@"
                    SELECT COUNT(*)
                    FROM UsuarioEtiquetas
                    WHERE idUsuario = @IdUsuario", new { IdUsuario = id });

                if (tieneEtiquetas == 0)
                {
                    var ids = connection.Query<int>("SELECT idEtiqueta FROM Etiquetas").ToList();
                    if (ids.Any())
                    {
                        int idEtiqueta = ids.OrderBy(_ => Guid.NewGuid()).First();
                        connection.Execute(
                            "INSERT INTO UsuarioEtiquetas (idUsuario, idEtiqueta) VALUES (@idUsuario, @idEtiqueta)",
                            new { idUsuario = id, idEtiqueta });
                    }
                }

                usuario.etiquetas = connection.Query<Etiqueta>(@"
                    SELECT e.*
                    FROM Etiquetas e
                    INNER JOIN UsuarioEtiquetas ue ON e.idEtiqueta = ue.idEtiqueta
                    WHERE ue.idUsuario = @IdUsuario
                    ORDER BY e.nombreEtiqueta", new { IdUsuario = id }).ToList();

                usuario.videos = new List<Video>();
            }

            return usuario;
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

        public void AsegurarEtiquetasParaTodosLosUsuarios()
        {
            var usuarios = ObtenerUsuarios();
            foreach (var usuario in usuarios)
            {
                AsegurarEtiquetaAleatoria(usuario.idUsuario);
            }
        }

        public List<int> ObtenerEtiquetasAleatorias(int cantidad = 1)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var ids = connection.Query<int>("SELECT idEtiqueta FROM Etiquetas").ToList();
                if (!ids.Any())
                {
                    return new List<int>();
                }

                return ids
                    .OrderBy(_ => Guid.NewGuid())
                    .Take(Math.Max(1, cantidad))
                    .ToList();
            }
        }

        public void LimpiarEtiquetasDuplicadas()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(@"
                    WITH duplicados AS (
                        SELECT idUsuario, idEtiqueta,
                               ROW_NUMBER() OVER (
                                   PARTITION BY idUsuario, idEtiqueta
                                   ORDER BY idUsuario
                               ) AS rn
                        FROM UsuarioEtiquetas
                    )
                    DELETE FROM duplicados
                    WHERE rn > 1;");
            }
        }
    }
}
