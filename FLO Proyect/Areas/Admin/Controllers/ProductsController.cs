using FLO_Proyect.Extensions;
using FLO_Proyect.Models;
using FLO_Proyect.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System;

namespace FLO_Proyect.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

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
            return View(appdbContext.Products.Include(x => x.SizeToProducts).Include(x => x.ColorToProducts).Include(x => x.Category).Where(x => x.Ischeck == true).ToList());

        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Categories = appdbContext.Categories.ToList();
            //ViewBag.Colors = appdbContext.Colors.ToList();
            ViewBag.Size = appdbContext.Sizes.ToList();

            var categories = appdbContext.Categories.ToList();
            ViewBag.Category = categories;

            var colors = appdbContext.Colors.ToList();
            //var sizes = appdbContext.Sizes.ToList();
            ViewBag.Colors = colors;
            //ViewBag.Sizes = sizes;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductVM model)
        {

            //TEK SEKILIN BAZAYA HEMDE PAPKAYA YUKLENMESI
            if (!model.Product.ImgUrlBaseFile.IsImage())
            {
                ModelState.AddModelError("Photo", "Image type is not valid");
                return View(model);
            }
            string filename = await model.Product.ImgUrlBaseFile.SaveFileAsync(_env.WebRootPath, "UploadProducts");

            model.Product.ImgUrlBase = filename;

            await appdbContext.Products.AddAsync(model.Product);
            await appdbContext.SaveChangesAsync();

            if (model.Product.ImagesFile != null)
            {
                foreach (var img in model.Product.ImagesFile)
                {
                    if (!img.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Image type is not valid");
                        return View(model);
                    }
                    string filename2 = await img.SaveFileAsync(_env.WebRootPath, "UploadProducts");

                    Images images = new Images
                    {
                        ProductId = model.Product.Id,
                        ImgUrl = filename2
                    };
                    appdbContext.Images.Add(images);
                }
            }
            foreach (var colorId in model.ColorIds)
            {
                var productColor = new ColorToProduct
                {
                    ProductId = model.Product.Id,
                    ColorId = colorId
                };
                appdbContext.ColorToProduct.Add(productColor);
            }

            foreach (var sizeId in model.SizeIds)
            {
                var productSize = new SizeToProduct
                {
                    ProductId = model.Product.Id,
                    SizeId = sizeId
                };
                appdbContext.SizeToProduct.Add(productSize);
            }
            appdbContext.SaveChanges();
            return RedirectToAction("Index");

        }
        //private IEnumerable<Size> GetSizes()
        //{
        //    // Mock data or retrieve from the database
        //    return new List<Size>
        //{
        //    new Size { Id = 1, Name =36},
        //    new Size { Id = 2, Name =37},
        //    new Size { Id = 3, Name =38}
        //};
        //}

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Category = appdbContext.Categories.ToList();
            ViewBag.Colors = appdbContext.Colors.ToList();
            ViewBag.Size = appdbContext.Sizes.ToList();



            var colorIds = appdbContext.ColorToProduct.Where(pc => pc.ProductId == id).Select(pc => pc.ColorId).ToList();
            var sizeIds = appdbContext.SizeToProduct.Where(ps => ps.ProductId == id).Select(ps => ps.SizeId).ToList();

            var products = await appdbContext.Products.Include(x => x.Images).Include(p => p.SizeToProducts).FirstOrDefaultAsync(p => p.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            var model = new ProductVM()
            {
                Product = products,
                ColorIds = colorIds,
                SizeIds = sizeIds
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM model)
        {
            //ViewBag.Category = appdbContext.Categories.ToList();
            var modelDb = appdbContext.Products.Include(x => x.ColorToProducts).Include(x => x.Category).Where(x => x.Ischeck == true).FirstOrDefault(u => u.Id == model.Product.Id);
            modelDb.Title = model.Product.Title;
            modelDb.Price = model.Product.Price;
            modelDb.OldPrice = model.Product.OldPrice;
            modelDb.CategoryId = model.Product.CategoryId;
            modelDb.Description = model.Product.Description;
            appdbContext.SaveChanges();
            var oldColors = appdbContext.ColorToProduct.Where(pc => pc.ProductId == model.Product.Id).ToList();
            var oldSizes = appdbContext.SizeToProduct.Where(ps => ps.ProductId == model.Product.Id).ToList();
            appdbContext.ColorToProduct.RemoveRange(oldColors);
            appdbContext.SizeToProduct.RemoveRange(oldSizes);
            await appdbContext.SaveChangesAsync();

            foreach (var colorId in model.ColorIds)
            {
                var productColor = new ColorToProduct
                {
                    ProductId = model.Product.Id,
                    ColorId = colorId
                };
                appdbContext.ColorToProduct.Add(productColor);
            }

            foreach (var sizeId in model.SizeIds)
            {
                var productSize = new SizeToProduct
                {
                    ProductId = model.Product.Id,
                    SizeId = sizeId
                };
                appdbContext.SizeToProduct.Add(productSize);
            }


            if (model.Product.ImgUrlBaseFile != null)
            {
                if (!model.Product.ImgUrlBaseFile.IsImage())
                {
                    ModelState.AddModelError("Photo", "Image type is not valid");
                    return View(model);
                }
                string filename = await model.Product.ImgUrlBaseFile.SaveFileAsync(_env.WebRootPath, "UploadProducts");

                modelDb.ImgUrlBase = filename;
                appdbContext.SaveChanges();
            }


            if (model.Product.ImagesFile != null)
            {
                foreach (var item in model.Product.ImagesFile)
                {
                    if (!item.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Image type is not valid");
                        return View(model);
                    }
                    string filename2 = await model.Product.ImgFile.SaveFileAsync(_env.WebRootPath, "UploadProducts");

                    Images images = new Images
                    {
                        ProductId = model.Product.Id,
                        ImgUrl = filename2
                    };
                    appdbContext.Images.Add(images);
                    appdbContext.SaveChanges();

                }
            }

            return RedirectToAction("Index");

        }

        public JsonResult DeleteImage(int id)
        {
            if (id != 0)
            {
                var model = appdbContext.Images.Find(id);
                appdbContext.Images.Remove(model);
                appdbContext.SaveChanges();
            }
            return Json(new
            {
                status = 200
            });
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
