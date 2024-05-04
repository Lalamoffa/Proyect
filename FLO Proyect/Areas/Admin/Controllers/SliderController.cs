using FLO_Proyect.Extensions;
using FLO_Proyect.Models;
using Microsoft.AspNetCore.Mvc;

namespace FLO_Proyect.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {

        private readonly AppdbContext context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppdbContext _context, IWebHostEnvironment env)
        {
            context = _context;
            _env = env;
        }

        public IActionResult GetSliders()
        {
            return View(context.Sliders.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //sehifeye 1000 cox sorgu gonderile biler sehife coker onun qarsisini alir.


        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            if (!slider.ImgFile.IsImage())
            {
                ModelState.AddModelError("Photo", "Image type is not valid");
                return View(slider);
            }
            string filename = await slider.ImgFile.SaveFileAsync(_env.WebRootPath, "UploadSlider");

            slider.ImgUrl = filename;


            context.Sliders.Add(slider);
            context.SaveChanges();
            return RedirectToAction("GetSliders");
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
            var slider = context.Sliders.Find(id);
            if (slider != null)
            {
                slider.Ischeck = true;
                context.SaveChanges();
            }

            return Json(new
            {
                status = 200
            });
        }

        [HttpGet]
        public JsonResult Edit(int id)
        {
            if (id == 0)
            {
                return Json(new
                {
                    Status = 400
                });
            }
            var model = context.Sliders.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return Json(new
                {
                    Status = 400
                });
            }
            return Json( model);
        }


        [HttpPost]
        public async Task<IActionResult> EditAsync(Slider slider)
        {
            var oldSlider = context.Sliders.Find(slider.Id);
            //if (!ModelState.IsValid)
            //{
            //    return View(slider);
            //}
            if (slider.ImgFile != null)
            {

                if (!slider.ImgFile.IsImage())
                {
                    ModelState.AddModelError("Photo", "Image type is not valid");
                    return View(slider);
                }
                string filename = await slider.ImgFile.SaveFileAsync(_env.WebRootPath, "UploadSlider");

                oldSlider.ImgUrl = filename;
            }
            oldSlider.SubTitle = slider.SubTitle;
            oldSlider.Title = slider.Title;
            oldSlider.Description = slider.Description;
            oldSlider.Ischeck = slider.Ischeck;

            context.SaveChanges();
            return RedirectToAction("GetSliders");
        }
    }

}
