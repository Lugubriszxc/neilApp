using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using neilApp.Models;

namespace neilApp.Controllers
{
    [Route("[controller]")]
    [Route("[controller]/[action]")]
    public class CrudController : Controller
    {
        private readonly ILogger<CrudController> _logger;

        private readonly prelimcrudContext _context;

        public CrudController(prelimcrudContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category ctg)
        {
            _context.Categories.Add(ctg);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if(ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Update(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }
            
            var category = _context.Categories.Where(q => q.Id == id).FirstOrDefault();
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            if(id != 0)
            {
                var categoryDelete = _context.Categories.Where(q => q.Id == id).FirstOrDefault();
                _context.Categories.Remove(categoryDelete);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

           return RedirectToAction("Index");
        }
    }
}