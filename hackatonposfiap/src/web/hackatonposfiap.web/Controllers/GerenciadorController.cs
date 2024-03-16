using Microsoft.AspNetCore.Mvc;

namespace hackatonposfiap.web.Controllers
{
    public class GerenciadorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
