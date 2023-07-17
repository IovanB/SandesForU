using Microsoft.AspNetCore.Mvc;
using SandesForU.Models;
using SandesForU.Repositories.Interfaces;
using SandesForU.ViewModels;
using System.Diagnostics;

namespace SandesForU.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository lancheRepository;

        public HomeController(ILancheRepository  lancheRepository)
        {
            this.lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = lancheRepository.LanchesPreferidos
            };

            return View(homeViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}