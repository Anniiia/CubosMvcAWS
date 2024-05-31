using CubosMvcAWS.Models;
using CubosMvcAWS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CubosMvcAWS.Controllers
{
    public class CubosController : Controller
    {

        private ServiceCubos service;

        public CubosController(ServiceCubos service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Cubo> cubos = await this.service.GetCubosAsync();
            return View(cubos);
        }

        public async Task<IActionResult> CubosMarca()
        {
            List<string> marcas = await this.service.GetMarcasAsync();
            ViewData["MARCAS"] = marcas;

            return View();
        }
    

        [HttpPost]
        public async Task<IActionResult> CubosMarca(string marca)
        {
            List<string> marcas = await this.service.GetMarcasAsync();
            ViewData["MARCAS"] = marcas;
            List<Cubo> cubos =  await this.service.GetCubosMarcaAsync(marca);
            return View(cubos);
        }

        public async Task<IActionResult> Details(int id)
        {
            Cubo cubo = await this.service.FindCubo(id);
            return View(cubo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cubo cubo)
        {
            await this.service.CreateCuboAsync(cubo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Cubo cubo = await this.service.FindCubo(id);
            return View(cubo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cubo cubo)
        {
            await this.service.UpdateCuboAsync(cubo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteCuboAsync(id);
            return RedirectToAction("Index");
        }

    }
}
