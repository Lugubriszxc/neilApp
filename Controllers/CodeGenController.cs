using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using neilApp.Models;
using neilApp.ViewModel;

namespace neilApp.Controllers
{
    //[Route("[controller]/[action]")]
    public class CodeGenController : Controller
    {
        private readonly prelimcrudContext _context;

        public CodeGenController(prelimcrudContext context)
        {
            _context = context;
        }

        // GET: CodeGen
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Products.ToListAsync());

            var res = 
            (
                from c in _context.Categories
                join p in _context.Products
                on c.Id.ToString() equals p.Category

                select new categoryProdView
                {
                    Id = p.Id,
                    Category = c.Id,
                    CategoryName = c.Name,
                    Name = p.Name,
                    Units = p.Units,
                    Stock = p.Stock,
                    Price = p.Price,
                    Status = p.Status
                }
            ).ToList();

            return View(res);
        }

        // GET: CodeGen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: CodeGen/Create
        public IActionResult Create()
        {
            ViewBag.categories = _context.Categories.ToList()
            .Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()});
            return View();
        }

        // POST: CodeGen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,Name,Units,Stock,Price,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                if(string.IsNullOrEmpty(product.Status) || product.Status == "false")
                {
                    product.Status = "Inactive";
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: CodeGen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.categories = _context.Categories.ToList()
                .Select(x => new SelectListItem{ Text = x.Name, Value = x.Id.ToString()});

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> AddStock(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStock(int id, int cStock, string dateVal, [Bind("Id,Category,Name,Units,Stock,Price,Status")] Product product, Stockhistory stockhistory)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if(product.Stock <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            if(ModelState.IsValid || product.Stock != 0)
            {
               try
                {
                    int added_stock = product.Stock;

                    product.Stock += cStock;
                    _context.Update(product);

                    stockhistory.AddedStock = added_stock;
                    stockhistory.ProdId = product.Id;
                    stockhistory.Date = dateVal;
                    _context.Add(stockhistory);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> AddedStockHistory(int? id)
        {
            var res = _context.Stockhistories.ToList().Where(p => p.ProdId == id);
            if(res.Count() == 0)
            {
                ViewBag.sList = "EMPTY LIST";
            }
            
            var pName = _context.Products.FirstOrDefault(p => p.Id == id);
            if (pName == null)
            {
                return NotFound();
            }

            ViewBag.nameProd = pName;
            return View(res);
        }

        // POST: CodeGen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Name,Units,Stock,Price,Status")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid || string.IsNullOrEmpty(product.Status) || product.Status == "false")
            {
                try
                {
                    if(string.IsNullOrEmpty(product.Status) || product.Status == "false"){
                     product.Status = "Inactive";
                    }
                    else{
                     product.Status = "Active";
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: CodeGen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: CodeGen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
