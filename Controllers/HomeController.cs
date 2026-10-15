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

    public IActionResult VistaIniciarSesion(){
        ViewBag.msj = "";
        return View("IniciarSesion");
    }

    [HttpPost]
    public IActionResult IniciarSesion(string email, string contraseña)
    {
        BD bd = new BD();
        HttpContext.Session.SetString("id", bd.BuscarSesion(email, contraseña));
        if (HttpContext.Session.GetString("id") == "-1"){
            ViewBag.msj = "No existe ese usuario :)";
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
        return View();
    }

    
    public IActionResult VistaRegistrarseConMensaje(){
        BD bd = new BD();
        ViewBag.etiquetas = bd.MostrarEtiquetas();
        ViewBag.msj = "El nombre de usuario o el email ya están en uso, por favor elija otros.";
        return View("Registrarse");
    }

    [HttpPost]
    public IActionResult RegistrarDatosPrincipales(string email, string contraseña, string apodo, int edad, string genero, List<int> IdsEtiquetas){
        BD bd = new BD();
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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
