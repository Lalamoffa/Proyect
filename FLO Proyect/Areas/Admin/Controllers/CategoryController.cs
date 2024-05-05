using FLO_Proyect.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FLO_Proyect.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppdbContext appdbContext;
        public CategoryController(AppdbContext _appdbContext)
        {
            appdbContext = _appdbContext;
        }

        public IActionResult Index()
        {
            return View(appdbContext.Categories.ToList());
        }

        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
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
                appdbContext.Categories.Remove(category);
                appdbContext.SaveChanges();
            }

            return Json(new
            {
                status = 200
            });
        }
    }
}
