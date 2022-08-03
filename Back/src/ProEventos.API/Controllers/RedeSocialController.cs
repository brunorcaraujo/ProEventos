using Microsoft.AspNetCore.Mvc;

namespace ProEventos.API.Controllers
{
    public class RedeSocialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
