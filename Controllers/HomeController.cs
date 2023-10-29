using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Passcode.Models;

namespace Passcode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if(HttpContext.Session.GetInt32("Tries") == null){
            HttpContext.Session.SetInt32("Tries", 1);
        }
        if(HttpContext.Session.GetString("Passcode") == null){
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string passcode = "";
        Random rand = new Random();
        for(int i = 0; i < 14; i++){
            passcode += chars[rand.Next(0, chars.Length)];
        }
        HttpContext.Session.SetString("Passcode", passcode);
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Generate()
    {
        int? tries = HttpContext.Session.GetInt32("Tries");
        HttpContext.Session.SetInt32("Tries", (int)tries + 1);
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string passcode = "";
        Random rand = new Random();
        for(int i = 0; i < 14; i++){
            passcode += chars[rand.Next(0, chars.Length)];
        }
        HttpContext.Session.SetString("Passcode", passcode);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
