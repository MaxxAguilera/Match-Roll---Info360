namespace Match-Roll--info360.Models
{
    public class Usuario
    {
        public string apodo {get; set;}
        public string contraseña { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string tipoUsuario { get; set; }
        public int id { get; set; }
        public int idUbicacion { get; set; }
        public string email {get; set;}
        public string biografia {get; set;}
        public string instrumentoPrincipal {get;set;}
        public string nivelExperiencia {get; set;}
        public string genero {get; set;}

        public Usuario(string nombre, string apellido, string email, string contraseña,  string tipoUsuario, string biografia, string instrumentoPrincipal, string nivelExperiencia, int idUbicacion, string genero, string apodo)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.email = email;
            this.contraseña = contraseña;
            this.tipoUsuario = tipoUsuario;
            this.biografia = biografia;
            this.instrumentoPrincipal = instrumentoPrincipal;
            this.nivelExperiencia = nivelExperiencia;
            this.genero = genero;
            this.idUbicacion = idUbicacion;
            this.apodo = apodo;
        }
    }
}