using FLO_Proyect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace FLO_Proyect.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ColorController : Controller
    {
        private readonly AppdbContext _context;
        public ColorController(AppdbContext context)
        {
            _context = context;
        }
        public IActionResult GetColor()
        {
            return View(_context.Colors.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Colors color)
        {
            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction("GetColor");
        }
        public IActionResult Delete(int id)
        {
            var c = _context.Colors.Find(id);
            if (c != null)
            {
                _context.Colors.Remove(c);
            }
            _context.SaveChanges();
            return RedirectToAction("GetColor");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_context.Colors.Find(id));
        }
        [HttpPost]
        public IActionResult Edit(Colors color)
        {
            var c = _context.Colors.Find(color.Id);
            if (c != null)
            {
                c.Name = color.Name;
            }
            _context.SaveChanges();
            return RedirectToAction("GetColor");
        }
    }
}
