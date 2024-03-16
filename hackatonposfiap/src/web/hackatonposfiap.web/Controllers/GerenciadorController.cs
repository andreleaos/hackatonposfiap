using hackatonposfiap.domain.Dtos;
using hackatonposfiap.domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hackatonposfiap.web.Controllers
{
    public class GerenciadorController : Controller
    {
        private readonly IGerenciadorService _gerenciadorService;

        public GerenciadorController(IGerenciadorService gerenciadorService)
        {
            _gerenciadorService = gerenciadorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Process()
        {
            return View();
        }

        public async Task<List<GerenciadorImagemDto>> Gereciador()
        {
            return await _gerenciadorService.GetAll() ;
        }



    }
}
