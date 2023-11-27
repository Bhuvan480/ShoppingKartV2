using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingKart.Data;
using ShoppingKart.Models;
using System;
using System.Diagnostics;

namespace ShoppingKart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductDbContext _productDb;

        public HomeController(ProductDbContext productDb)
        {
            _productDb = productDb;
        }

        public IActionResult Index()
        {
            try
            {
                var products = _productDb.Products.ToList();
                ViewBag.Products = products;
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }

        public IActionResult Pagination(string page = "1")
        {
            try
            {
                var PageSize = 8;
                int PageNumber = int.Parse(page);
                var products = _productDb.Products.FromSqlInterpolated($"ProductPagination {PageSize}, {PageNumber}").ToList();

                var TotalProducts = _productDb.Database.SqlQuery<int>($"TotalProductCount").ToList().FirstOrDefault();

                //var currentPage = int.Parse(page) > 0 && int.Parse(page) <= totalPages ? page : "1";
                //var PreviousPage = int.Parse(currentPage) - 1;
                //var NextPage = int.Parse(currentPage) + 1;

                ViewData["TotalProducts"] = TotalProducts;
                ViewData["CurrentPage"] = PageNumber;
                ViewBag.Products = products;
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }

        public IActionResult ClientPagination()
        {
            return View();
        }

        public IActionResult GetProducts()
        {
            return Json(_productDb.Products.ToList());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}