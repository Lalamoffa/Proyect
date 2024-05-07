using FLO_Proyect.Extensions;
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
        private readonly IWebHostEnvironment _env;

        public ProductsController(AppdbContext _appdbContext, IWebHostEnvironment env)
        {
            appdbContext = _appdbContext;
            _env = env;
        }


        public IActionResult Index()
        {
            return View(appdbContext.Products.Include(x => x.Category).Where(x => x.Ischeck == true).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Category = appdbContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Products model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Category = appdbContext.Categories.ToList();
                return View(model);
            }


            //TEK SEKILIN BAZAYA HEMDE PAPKAYA YUKLENMESI
            if (!model.ImgUrlBaseFile.IsImage())
            {
                ModelState.AddModelError("Photo", "Image type is not valid");
                return View(model);
            }
            string filename = await model.ImgUrlBaseFile.SaveFileAsync(_env.WebRootPath, "UploadProducts");

            model.ImgUrlBase = filename;

            appdbContext.Products.Add(model);
            appdbContext.SaveChanges();

            if (model.ImagesFile != null)
            {
                foreach (var item in model.ImagesFile)
                {
                    if (!item.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Image type is not valid");
                        return View(model);
                    }
                    string filename2 = await item.SaveFileAsync(_env.WebRootPath, "uploadProducts");

                    Images images = new Images
                    {
                        ProductId = model.Id,
                        ImgUrl = filename2
                    };
                }
            }
            appdbContext.Products.Add(model);
            appdbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //public IActionResult Edit(int id)
        //{
        //    ViewBag.Category = appdbContext.Categories.ToList();
        //    var model = appdbContext.Products.FirstOrDefault(x => x.Id == id);

        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult Edit(Products products)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(products);
        //    }
        //    appdbContext.Products.Update(products);
        //    appdbContext.SaveChanges();
        //    return RedirectToAction("Index");
        //}



        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    if (id == 0)
        //    {
        //        return NotFound();
        //    }

        //    var model = appdbContext.Products.FirstOrDefault(x => x.Id == id);
        //    if (model == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}


        //[HttpPost]
        //public async Task<IActionResult> EditAsync(Products products)
        //{
        //    var oldProducts = appdbContext.Products.Find(products.Id);
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return View(slider);
        //    //}
        //    if (products.ImgFile != null)
        //    {

        //        if (!products.ImgFile.IsImage())
        //        {
        //            ModelState.AddModelError("Photo", "Image type is not valid");
        //            return View(products);
        //        }
        //        string filename = await products.ImgFile.SaveFileAsync(_env.WebRootPath, "UploadSlider");

        //        oldProducts.ImgUrl = filename;
        //    }
        //    oldProducts.Title = products.Title;
        //    oldProducts.Description = products.Description;
        //    oldProducts.Price = products.Price;
        //    oldProducts.Category = products.Category;
        //    oldProducts.Ischeck = products.Ischeck;

        //    appdbContext.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        public IActionResult Edit(int id)
        {
            ViewBag.Category = appdbContext.Categories.ToList();
            var model = appdbContext.Products.FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Products products)
        {
            ViewBag.Category = appdbContext.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View(products);
            }
            appdbContext.Products.Update(products);
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

            var product = appdbContext.Products.Find(id); //axtarib tapiram
            if (product != null)
            {
                product.Ischeck = false;
                appdbContext.SaveChanges();
            }

            return Json(new
            {
                status = 200
            });

        }
    }
}
