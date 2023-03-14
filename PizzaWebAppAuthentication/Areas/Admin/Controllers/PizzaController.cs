﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;
using PizzaWebAppAuthentication.Repositories;

namespace PizzaWebAppAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PizzaController: Controller
    {
        private readonly ApplicationDbContext _contextDb;
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PizzaController(ApplicationDbContext contextDb, 
                              IPizzaRepository pizzaRepository,
                              IWebHostEnvironment webHostEnvironment)
        {
            _contextDb = contextDb;
            _pizzaRepository = pizzaRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pizzaRepository.GetStandartPizzas());
        }
        
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                var name = await _contextDb.Pizzas.FirstOrDefaultAsync(p => p.Name == pizza.Name);
                if (name != null) 
                {
                    ModelState.AddModelError("", $"Pizza {pizza.Name} already exists");
                    return View(pizza);
                }

                if (pizza.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string imageName = Guid.NewGuid().ToString() + "_" + pizza.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fileStream = new(filePath, FileMode.Create);
                    await pizza.ImageUpload.CopyToAsync(fileStream);
                    fileStream.Close();

                    pizza.ImagePath = imageName;
                }

                _contextDb.Add(pizza);
                await _contextDb.SaveChangesAsync();

                TempData["Success"] = $"Pizza {pizza.Name} has been created";

                return RedirectToAction("Index");
            }

            return View(pizza);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            Pizza pizza = await _contextDb.Pizzas.FindAsync(id);

            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                var name = await _contextDb.Pizzas.Where(p => p.Name == pizza.Name)
                                                  .Where(i => i.Id != pizza.Id)
                                                  .Select(p=>pizza.Name)
                                                  .FirstOrDefaultAsync();
                if (name != null)
                {
                    ModelState.AddModelError("", $"Pizza {pizza.Name} already exists");
                    return View(pizza);
                }

                if (pizza.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string imageName = Guid.NewGuid().ToString() + "_" + pizza.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fileStream = new(filePath, FileMode.Create);
                    await pizza.ImageUpload.CopyToAsync(fileStream);
                    fileStream.Close();

                    pizza.ImagePath = imageName;
                }

                _contextDb.Update(pizza);
                await _contextDb.SaveChangesAsync();

                TempData["Success"] = $"Pizza {pizza.Name} has been edited";                
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            Pizza pizza = await _contextDb.Pizzas.FindAsync(id);

            _contextDb.Pizzas.Remove(pizza);
            await _contextDb.SaveChangesAsync();

            TempData["Success"] = $"Pizza {pizza.Name} has been deleted";

            return RedirectToAction("Index");
        }
    }
}
