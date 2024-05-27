using FLO_Proyect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FLO_Proyect.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        private readonly AppdbContext appdbContext;
        public CategoryController(AppdbContext _appdbContext)
        {
            appdbContext = _appdbContext;
        }

        public IActionResult Index()
        {
            return View(appdbContext.Categories.Where(x => x.Ischeck == true).ToList());
        }


        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            appdbContext.Categories.Add(category);
            appdbContext.SaveChanges();
            return RedirectToAction("Index");

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
            var category = appdbContext.Categories.Find(id);
            if (category != null)
            {
                category.Ischeck = false;
                appdbContext.SaveChanges();
            }

            return Json(new
            {
                status = 200
            });
        }

        [HttpGet]
        public JsonResult Edit(int id)
        {
            var category = appdbContext.Categories.Find(id); // Kategori verilerini veritabanından al
            return Json(category); // Kategori verilerini JSON formatında dön
        }

        [HttpPost]
        public IActionResult Edit(Category model)
        {
            // // ViewBag.Category = appDbContext.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            appdbContext.Categories.Update(model);
            appdbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
