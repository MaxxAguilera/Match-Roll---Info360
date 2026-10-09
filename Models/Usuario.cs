namespace MatchandRoll.Models
{
    public class Usuario
    {
        public int id { get; set; }
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
        public List<ProyectoMusical> proyectos { get; set; } = new List<ProyectoMusical>();
        public List<Match> matchesSolicitante { get; set; } = new List<Match>();
        public List<Match> matchesReceptor { get; set; } = new List<Match>();
        public Ubicacion ubicacion { get; set; } = new Ubicacion();
        public List<string> etiquetas { get; set; } = new List<string>();

        public Usuario()
        {
        }

        public Usuario(string email, string contraseña, string apodo, int edad, string genero, string biografia, string instrumentoPrincipal, string nivelExperiencia, int idUbicacion)
        {
            this.email = email;
            this.contraseña = contraseña;
            this.apodo = apodo;
            this.edad = edad;
            this.genero = genero;
            this.biografia = biografia;
            this.instrumentoPrincipal = instrumentoPrincipal;
            this.nivelExperiencia = nivelExperiencia;
            this.idUbicacion = idUbicacion;
        }
    }
}
