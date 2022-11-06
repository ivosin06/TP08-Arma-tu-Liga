using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP08.Models;
using Microsoft.AspNetCore.Hosting;

namespace TP08.Controllers;

public class HomeController : Controller
{
    private IWebHostEnvironment Environment;

    public HomeController(IWebHostEnvironment environment)
    {
        Environment = environment;
    }

        public IActionResult Index()
    {
        
        return View();
    }

    public IActionResult verLigas()
    {   
        ViewBag.ListaL = BD.ListarLigas();
        return View();
    }
    public IActionResult verDetalleLiga(int IdLiga)
    {   
        ViewBag.Liga = BD.verInfoLiga(IdLiga);
        ViewBag.ListaEquipos = BD.ListarEquipos(IdLiga);
        return View();
    }

    public IActionResult AgregarLiga()
    {
        return View();        
    }

    [HttpPost]
    public IActionResult GuardarLiga(Liga L)
    {
        BD.AgregarLiga(L);
        L.IdLiga = BD.MaxIdL();
        return RedirectToAction("AgregarEquipo", new { IdLiga = L.IdLiga });   
    }
    public IActionResult AgregarEquipo(int IdLiga)
    {
        ViewBag.IdL=IdLiga;
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GuardarEquipo(int IdLiga, Equipo E)
    {
        Console.WriteLine(E.Nombre);
        E.IdLiga = IdLiga;
        BD.AgregarEquipo(E);
        return RedirectToAction("AgregarEquipo", new { IdLiga = IdLiga });  
    }

    public IActionResult FinalizarLiga(int IdLiga)
    {
        Liga ligaAFinalizar = BD.verInfoLiga(IdLiga);
        ligaAFinalizar.Equipos = BD.ListarEquipos(IdLiga);
        ligaAFinalizar.ArmarFixture();
        foreach (Fecha fecha in ligaAFinalizar.Fixture)
        {
            BD.AgregarFecha(fecha, IdLiga);
        }
        return RedirectToAction("Index");  
    }
    public IActionResult ModificarResultado(int IdLiga, int IdEquipoLocal, int IdEquipoVisitante){
        ViewBag.IdLiga=IdLiga;
        ViewBag.IdEquipoLocal = IdEquipoLocal;
        ViewBag.IdEquipoVisitante = IdEquipoVisitante;
        return View();  
    }
    public IActionResult EditarResultado(int IdLiga, int IdEquipoLocal, int IdEquipoVisitante, int golesEquipoLocal, int golesEquipoVisitante){
        BD.ModificarResultado(IdEquipoLocal, IdEquipoVisitante, golesEquipoLocal, golesEquipoVisitante);
        return RedirectToAction("VerFixture", new { IdLiga = IdLiga });
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult VerFixture(int IdLiga)
    {
        List<Partido> partidos = BD.VerInfoPartidos(IdLiga);
        ViewBag.IdLiga = IdLiga;
        ViewBag.Partidos = partidos;
        return View();
    }
    public IActionResult BorrarLiga(int IdLiga){
        BD.BorrarLiga(IdLiga);
        return RedirectToAction("VerLigas", new { IdLiga = IdLiga });
    }
}
