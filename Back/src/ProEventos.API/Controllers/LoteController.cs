using Microsoft.AspNetCore.Mvc;

namespace ProEventos.API.Controllers
{
    public class LoteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
