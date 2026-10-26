namespace MatchRollInfo360.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string apodo { get; set; } = string.Empty;
        public string contraseña { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string tipoUsuario { get; set; } = string.Empty;
        public int idUbicacion { get; set; }
        public string email { get; set; } = string.Empty;
        public string biografia { get; set; } = string.Empty;
        public string instrumentoPrincipal { get; set; } = string.Empty;
        public string nivelExperiencia { get; set; } = string.Empty;
        public string genero { get; set; } = string.Empty;
        public int edad { get; set; }
        public string fotoPerfil { get; set; } = string.Empty;
        public List<ProyectoMusical> proyectos { get; set; } = new List<ProyectoMusical>();
        public List<Match> matchesSolicitante { get; set; } = new List<Match>();
        public List<Match> matchesReceptor { get; set; } = new List<Match>();
        public Ubicacion ubicacion { get; set; } = new Ubicacion();
        public List<Etiqueta> etiquetas { get; set; } = new List<Etiqueta>(); 
        public string videoPrincipal { get; set; } = string.Empty;

        public Usuario()
        {
        }

        public Usuario(string email, string contraseña, string apodo, int edad, string genero, List<Etiqueta> etiquetas)
        {
            this.email = email;
            this.contraseña = contraseña;
            this.apodo = apodo;
            this.edad = edad;
            this.genero = genero;
            this.etiquetas = etiquetas;
        }

        public Usuario(string apodo, string biografia, string nombre, string apellido, int edad, string genero, string email, string instrumentoPrincipal, string nivelExperiencia, string tipoUsuario, string contraseña, int idUbicacion, string videoPrincipal)
        {
            this.apodo = apodo;
            this.contraseña = contraseña;
            this.nombre = nombre;
            this.apellido = apellido;
            this.tipoUsuario = tipoUsuario;
            this.idUbicacion = idUbicacion;
            this.email = email;
            this.biografia = biografia;
            this.instrumentoPrincipal = instrumentoPrincipal;
            this.nivelExperiencia = nivelExperiencia;
            this.genero = genero;
            this.edad = edad;
            this.videoPrincipal = videoPrincipal;
        }
    }
}
