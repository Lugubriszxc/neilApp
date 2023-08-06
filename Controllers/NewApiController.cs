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
namespace sampleMVC.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NewAPIController : ControllerBase
    {

        private readonly prelimcrudContext _context;
        public NewAPIController(prelimcrudContext context)
        {
            _context = context;
        }

        public ActionResult<List<Product>> getAllProducts(){
            
            //return _context.Products.ToList();
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

            return Ok(res);
        }

   
        public ActionResult<List<Category>> getAllCategories(){
            return _context.Categories.ToList();
        }

/*
        public IActionResult saveCategory(string name){
            Category c = new Category(){
                Name = name
            };
            _context.Categories.Add(c);
            _context.SaveChanges();
            return Ok();
        }*/

        public IActionResult addProduct(Product p)
        {
            if(string.IsNullOrEmpty(p.Status) || p.Status == "false")
            {
                p.Status = "Inactive";
            }
            _context.Products.Add(p);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult updateProduct(Product p, int oldStock)
        {
            int newStock = p.Stock;

            if(string.IsNullOrEmpty(p.Status) || p.Status == "false")
            {
                p.Status = "Inactive";
            }
            else
            {
                p.Status = "Active";
            }
            _context.Products.Update(p);
            _context.SaveChanges();


            //CHANGE MOCK TOTAL, STOCK

            Cart ct = new Cart();
            var res = _context.Carts.Where(q => q.ProdId == p.Id).FirstOrDefault();
            if(res != null)
            {
                if(newStock > oldStock)
                {
                    //add
                    int newMockStock = newStock - oldStock;
                    res.CMockStock += newMockStock;
                }
                else
                {
                    int newMockStock = oldStock - newStock;
                    res.CMockStock -= newMockStock;

                    if(res.CMockStock < 0)
                    {
                        res.CQuantity = newStock;
                    }   
                }

                res.CMockTotal = int.Parse(p.Price) * res.CQuantity;
                _context.Carts.Update(res);
                _context.SaveChanges();
            }

            return Ok();
        }


        public IActionResult deleteProduct(int id)
        {
            var res = _context.Products.Where(q => q.Id == id).FirstOrDefault();
            _context.Products.Remove(res);
            _context.SaveChanges();
            
            return Ok();
        }

        public IActionResult addStockProduct(Product p, int iStock,string date)
        {
            p.Stock += iStock;
            _context.Products.Update(p);
            _context.SaveChanges();


            Stockhistory sh = new Stockhistory();   
            sh.AddedStock = iStock;
            sh.ProdId = p.Id;
            sh.Date = date;

            _context.Stockhistories.Add(sh);
            _context.SaveChanges();


            //ADD MOCK STOCK
            Cart ct = new Cart();
            var res = _context.Carts.Where(q => q.ProdId == p.Id).FirstOrDefault();
            if(res != null)
            {
                res.CMockStock += iStock;
                _context.Carts.Update(res);
                _context.SaveChanges();
            }

            return Ok();
        }

        public ActionResult<List<Stockhistory>> viewStock(int id){
            //return _context.Products.ToList();
            var res = 
            (
                from s in _context.Stockhistories
                join p in _context.Products
                on s.ProdId equals p.Id
                where p.Id == id

                select new stockProdHistory
                {
                    Id = p.Id,
                    Name = p.Name,
                    AStockId = s.AStockId,
                    ProdId = p.Id,
                    AddedStock = s.AddedStock,
                    Date = s.Date
                }
            ).ToList();

            return Ok(res);
        }

        public IActionResult addCart(Cart ct)
        {
            _context.Carts.Add(ct);
            _context.SaveChanges();
            return Ok(ct);
        }

        public IActionResult updateCart(Cart ct)
        {
            _context.Carts.Update(ct);
            _context.SaveChanges();
            return Ok(ct);
        }

        public ActionResult<List<Cart>> popCart(){
            var res = 
            (
                from cp in _context.Carts
                join p in _context.Products on cp.ProdId equals p.Id
                join c in _context.Categories on p.Category equals c.Id.ToString()

                select new cartList
                {
                    Id = p.Id,
                    Category = c.Id,
                    CategoryName = c.Name,
                    Name = p.Name,
                    Units = p.Units,
                    Stock = p.Stock,
                    Price = p.Price,
                    Status = p.Status,
                    Quantity = cp.CQuantity,
                    mTotal = cp.CMockTotal,
                    MockStock = cp.CMockStock,
                    cartID = cp.CartId
                }
            ).ToList();

            return Ok(res);
        }

        public IActionResult deleteCart(int cartID)
        {
            var res = _context.Carts.Where(q => q.CartId == cartID).FirstOrDefault();
            _context.Carts.Remove(res);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult savetoOrder(Order ord)
        {
            _context.Orders.Add(ord);
            _context.SaveChanges();

            //Get the current order id
            //var res = _context.Orders.LastOrDefault();
            var lastId = _context.Orders.OrderByDescending(x => x.OrderId).FirstOrDefault()?.OrderId;
            return Ok(lastId);
        }

        public IActionResult savetoOrderDetails(Orderdetail ordtls, int mStock)
        {
            _context.Orderdetails.Add(ordtls);
            _context.SaveChanges();


            //UPDATE THE STOCK
            Product p = new Product();
            var res = _context.Products.Where(q => q.Id == ordtls.ProductId).FirstOrDefault();
            res.Stock = mStock;

            _context.Products.Update(res);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult clearCart()
        {
            _context.Carts.RemoveRange(_context.Carts);
            _context.SaveChanges();
            return Ok();
        }

        public ActionResult<List<Order>> getAllOrders(){
            return  _context.Orders.ToList();
        }

        public ActionResult<List<Stockhistory>> getAllOrderDetails(int id){
            var res = 
            (
                from od in _context.Orderdetails
                join p in _context.Products on od.ProductId equals p.Id
                join o in _context.Orders on od.OrderId equals o.OrderId
                where o.OrderId == id

                select new orderInfo
                {
                    productName = p.Name,
                    prodUnit = p.Units,
                    prodPrice = p.Price,
                    OrderDetailsId = od.OrderDetailsId,
                    OrderId = o.OrderId,
                    ProductId = p.Id,
                    Quantity = od.Quantity,
                    ProductTotal = od.ProductTotal,
                    subTotal = o.SubTotal,
                    deduction = o.Deduction,
                    totalAmount = o.TotalAmount,
                    paidAmount = o.PaidAmount,
                    sukli = o.Sukli
                }
            ).ToList();

            return Ok(res);
        }
    }
}