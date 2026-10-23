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
        return RedirectToAction("Index");
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
    public IActionResult RegistrarDatosPrincipales(string email, string contraseña, string contraseñaConfirmacion, string apodo, int edad, string genero, List<int> IdsEtiquetas){
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
            List<int> idsEtiquetasFinales = (IdsEtiquetas != null && IdsEtiquetas.Any())
                ? IdsEtiquetas
                : bd.ObtenerEtiquetasAleatorias(1);

            List<Etiqueta> etiquetas = bd.DevolverEtiquetasPorIds(idsEtiquetasFinales);
            Usuario user = new Usuario(email, contraseña, apodo, edad, genero, etiquetas);
            bd.CrearUsuarioNoCompleto(user);
            HttpContext.Session.SetString("id", user.idUsuario.ToString());
            bd.CrearUsuarioEtiquetas(user.idUsuario, idsEtiquetasFinales);

            if (!idsEtiquetasFinales.Any())
            {
                bd.AsegurarEtiquetaAleatoria(user.idUsuario);
            }

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
    public IActionResult RegistrarDatosCompleto(Usuario usuario, List<int> IdsEtiquetas, List<Video> videos)
    {
        BD bd = new BD();

        if (string.IsNullOrWhiteSpace(usuario.genero))
        {
            usuario.genero = "Prefiero no decirlo";
        }
        else
        {
            usuario.genero = ObtenerGeneroValido(usuario.genero);
        }

        var usuarioActual = bd.MostrarUsuario(usuario.idUsuario);
        if (usuario.idUbicacion <= 0)
        {
            usuario.idUbicacion = usuarioActual.idUbicacion > 0 ? usuarioActual.idUbicacion : 1;
        }
        else if (usuario.idUbicacion > 0)
        {
            var ubicacionesValidas = bd.ObtenerUbicacionesDisponibles();
            if (!ubicacionesValidas.Contains(usuario.idUbicacion))
            {
                usuario.idUbicacion = usuarioActual.idUbicacion > 0 ? usuarioActual.idUbicacion : 1;
            }
        }

        if (usuario.idUbicacion <= 0 && usuarioActual.idUbicacion > 0)
        {
            usuario.idUbicacion = usuarioActual.idUbicacion;
        }

        if (usuario.idUbicacion <= 0)
        {
            usuario.idUbicacion = 1;
        }

        bool nombreValido = bd.ValidarNombreUsuario(usuario.apodo, usuario.idUsuario);
        bool emailValido = bd.ValidarEmail(usuario.email, usuario.idUsuario);

        if (nombreValido && emailValido)
        {
            if (usuario.edad < 1 || usuario.edad > 100)
            {
                ViewBag.etiquetas = bd.MostrarEtiquetas();
                ViewBag.msj = "La edad debe estar entre 1 y 100 años.";
                return RedirectToAction("VistaPerfil");
            }

            var urlsVideo = new List<string>();

            if (!string.IsNullOrWhiteSpace(usuario.urlVideo))
            {
                urlsVideo.Add(usuario.urlVideo);
            }

            if (videos != null)
            {
                urlsVideo.AddRange(videos
                    .Where(v => v != null)
                    .Select(v => v.urlVideo)
                    .Where(v => !string.IsNullOrWhiteSpace(v)));
            }

            usuario.urlVideo = urlsVideo
                .Select(v => v.Trim())
                .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(usuario.urlVideo))
            {
                bd.GuardarVideoDesdeUsuario(usuario);
            }

            if (videos != null && videos.Any())
            {
                bd.ActualizarVideosUsuario(usuario.idUsuario, videos);
            }

            bd.ActualizarUsuario(usuario);
            bd.ActualizarUsuarioEtiquetas(usuario.idUsuario, IdsEtiquetas);
            return RedirectToAction("Index");
        }

        ViewBag.etiquetas = bd.MostrarEtiquetas();
        ViewBag.msj = "Nombre y email ya están en uso, por favor elija otros.";
        return RedirectToAction("VistaPerfil");
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
        bd.LimpiarEtiquetasDuplicadas();
        bd.AsegurarEtiquetasParaTodosLosUsuarios();

        int idUsuarioActual = 0;
        int.TryParse(HttpContext.Session.GetString("id"), out idUsuarioActual);

        List<Usuario> usuarios = bd.ObtenerUsuarios()
            .Where(u => idUsuarioActual <= 0 || u.idUsuario != idUsuarioActual)
            .ToList();

        ViewBag.usuarios = usuarios
            .Select(u => bd.MostrarUsuarioCompleto(u.idUsuario) ?? u)
            .ToList();
        return View("Match");
    }
    public IActionResult VistaSalas(){
        return View("Salas");
    }
    public IActionResult VistaPerfil(){
        int id = int.Parse(HttpContext.Session.GetString("id"));
        BD bd = new BD();
        Usuario usuario = bd.MostrarUsuarioCompleto(id);
        ViewBag.usuario = usuario;
        ViewBag.etiquetas = bd.MostrarEtiquetas();
        ViewBag.etiquetasUsuario = bd.ObtenerEtiquetasPorUsuario(id);
        return View("Perfil");
    }
}
