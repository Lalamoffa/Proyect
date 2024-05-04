using FLO_Proyect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FLO_Proyect.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly AppdbContext appdbContext;
        public ProductsController(AppdbContext _appdbContext)
        {
            appdbContext = _appdbContext;
        }

        
        public IActionResult Index()
        {
          return View(appdbContext.Products.Include(x => x.Category).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Category = appdbContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Products model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Category = appdbContext.Categories.ToList();
                return View(model);
            }

            appdbContext.Products.Add(model);
            appdbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Category = appdbContext.Categories.ToList();
            var model = appdbContext.Products.FirstOrDefault(x => x.Id == id);

            return View(model);
        }

        public JsonResult Delete(int id)
        {
            if (id == 0)
            {
                return Json(new
                {
                    status = 400
                });
            }

            var product = appdbContext.Products.Find(id); //axtarib tapiram
            if (product != null)
            {
                appdbContext.Products.Remove(product);
                appdbContext.SaveChanges();
            }

            return Json(new
            {
                status = 200
            });

        }
    }
}
