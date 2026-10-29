using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MatchRollInfo360.Models;

namespace MatchRollInfo360.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        int id = int.Parse(HttpContext.Session.GetString("id") ?? "0");
        if (id == 0)
        {
            return RedirectToAction("IndexSinCuenta");
        }

        BD bd = new BD();
        ViewBag.user = bd.MostrarUsuario(id);
        return View();
    }

    public IActionResult IndexSinCuenta(){
        return View();
    }
    public IActionResult salas(){
        return View();
    }
    

    public IActionResult VistaIniciarSesion(){
        ViewBag.msj = "";
        ViewData["HideNav"] = true;
        return View("IniciarSesion");
    }

    [HttpPost]
    public IActionResult IniciarSesion(string email, string contraseña)
    {
        BD bd = new BD();
        HttpContext.Session.SetString("id", bd.BuscarSesion(email, contraseña));
        if (HttpContext.Session.GetString("id") == "-1"){
            ViewBag.msj = "No existe ese usuario :)";
            ViewData["HideNav"] = true;
            return View();
        }
        else{
            return RedirectToAction("Index");
        }
    }

    public IActionResult CerrarSesion(){
        HttpContext.Session.Clear();
        return RedirectToAction("IndexSinCuenta");
    }

    public IActionResult Registrarse(){
        BD bd = new BD();
        ViewBag.etiquetas = bd.MostrarEtiquetas();
        ViewBag.msj = null;
        ViewData["HideNav"] = true;
        return View();
    }

    
    public IActionResult VistaRegistrarseConMensaje(){
        BD bd = new BD();
        ViewBag.etiquetas = bd.MostrarEtiquetas();
        ViewBag.msj = "El nombre de usuario o el email ya están en uso, por favor elija otros.";
        ViewData["HideNav"] = true;
        return View("Registrarse");
    }

    [HttpPost]
    public IActionResult RegistrarDatosPrincipales(string email, string contraseña, string contraseñaConfirmacion, string apodo, int edad, string genero, List<int> IdsEtiquetas)
    {
        BD bd = new BD();

        if (string.IsNullOrWhiteSpace(genero))
        {
            ViewBag.etiquetas = bd.MostrarEtiquetas();
            ViewBag.msj = "Debes seleccionar un género para continuar.";
            ViewData["HideNav"] = true;
            return View("Registrarse");
        }

        genero = ObtenerGeneroValido(genero);

        if (edad < 1 || edad > 100){
            ViewBag.etiquetas = bd.MostrarEtiquetas();
            ViewBag.msj = "La edad debe estar entre 1 y 100 años.";
            ViewData["HideNav"] = true;
            return View("Registrarse");
        }

        if (contraseña != contraseñaConfirmacion){
            ViewBag.etiquetas = bd.MostrarEtiquetas();
            ViewBag.msj = "Las contraseñas no coinciden.";
            ViewData["HideNav"] = true;
            return View("Registrarse");
        }
    
        if (bd.ValidarNombreUsuario(apodo) && bd.ValidarEmail(email)){
            List<Etiqueta> etiquetas = bd.DevolverEtiquetasPorIds(IdsEtiquetas);
            Usuario user = new Usuario(email, contraseña, apodo, edad, genero, etiquetas);
            bd.CrearUsuarioNoCompleto(user);
            HttpContext.Session.SetString("id", user.idUsuario.ToString());
            bd.CrearUsuarioEtiquetas(user.idUsuario, IdsEtiquetas);
            return RedirectToAction("Index");
        }
        return RedirectToAction("VistaRegistrarseConMensaje");
    }

    private static string ObtenerGeneroValido(string genero)
    {
        if (string.IsNullOrWhiteSpace(genero))
            return "Prefiero no decirlo";

        var valor = genero.Trim();
        return valor switch
        {
            "1" => "Masculino",
            "2" => "Femenino",
            "3" => "No binario",
            "Masculino" => "Masculino",
            "Femenino" => "Femenino",
            "No binario" => "No binario",
            "Prefiero no decirlo" => "Prefiero no decirlo",
            _ => valor
        };
    }
    
    [HttpPost]
    public IActionResult RegistrarDatosCompleto(Usuario usuario, List<int> IdsEtiquetas)
    {
        BD bd = new BD();

        if (string.IsNullOrWhiteSpace(usuario.genero))
        {
            usuario.genero = bd.MostrarUsuario(usuario.idUsuario).genero;
        }
        else
        {
            usuario.genero = ObtenerGeneroValido(usuario.genero);
        }
        
        bool nombreValido = bd.ValidarCambiodeNombreUsuario(usuario.apodo);
        bool emailValido = bd.ValidarCambiodeEmail(usuario.email);

        if (nombreValido && emailValido)
        {
            if (usuario.edad < 1 || usuario.edad > 100)
            {
                ViewBag.etiquetas = bd.MostrarEtiquetas();
                ViewBag.msj = "La edad debe estar entre 1 y 100 años.";
                return RedirectToAction("VistaPerfil");
            }
            bd.ActualizarUsuario(usuario);
            bd.ActualizarUsuarioEtiquetas(usuario.idUsuario, IdsEtiquetas);
            return RedirectToAction("Index");
        }
        else
        {
            ViewBag.etiquetas = bd.MostrarEtiquetas();
            ViewBag.msj = "Nombre y email ya están en uso, por favor elija otros.";
            return RedirectToAction("VistaPerfil");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult VistaMatch(){
        BD bd = new BD();
        List<Usuario> usuarios = bd.ObtenerUsuarios();
        ViewBag.usuarios = usuarios;
        ViewBag.idUsuarioActual = HttpContext.Session.GetString("id");
        return View("Match");
    }
    public IActionResult VistaSalas(){
        return View("Salas");
    }
    public IActionResult VistaPerfil(){
        int id = int.Parse(HttpContext.Session.GetString("id"));
        BD bd = new BD();
        Usuario usuario = bd.MostrarUsuario(id);
        ViewBag.usuario = usuario;
        ViewBag.etiquetas = bd.MostrarEtiquetas();
        ViewBag.etiquetasUsuario = bd.ObtenerEtiquetasPorUsuario(id);
        List<int> idsEtiquetasUsuario = bd.ObtenerIdsEtiquetasPorUsuario(id);
        ViewBag.idsEtiquetasUsuario = idsEtiquetasUsuario;
        return View("Perfil");
    }
    [HttpPost]
    public IActionResult DarLike(int idUsuarioReceptor)
    {
        string? idSesion = HttpContext.Session.GetString("id");

        if (!int.TryParse(idSesion, out int idUsuarioSolicitante))
        {
            return Unauthorized();
        }

        if (idUsuarioSolicitante == idUsuarioReceptor)
        {
            return BadRequest();
        }

        BD bd = new BD();

        bool hayMatch = bd.DarLike(idUsuarioSolicitante, idUsuarioReceptor);
        return Json(new { hayMatch });
    }
}
