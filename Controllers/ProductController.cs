using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Repository.IRepository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BulkyBookWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;

        // GET: /<controller>/

        public ProductController(IProductRepository db)
        {

            _productRepo = db;
        }
        public IActionResult Index()
        {
            Console.WriteLine("Hello World!");
            List<Product>? objProductList = _productRepo.GetAll().ToList();
            //List<Category> objCategoryList = _db.Categories.ToList();
            return View(objProductList);
        }



        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {

          

            if (ModelState.IsValid)
            {
                _productRepo.Add(obj);
                _productRepo.Save();
                TempData["success"] = "Product created successfully";
                return View();
            }
            return View();
        }



        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
      Product? productFromDb = _productRepo.Get(u => u.ProductId == id);
            // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            // Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault() ; 

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);


        }


        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _productRepo.Update(obj);
                _productRepo.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }



        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _productRepo.Get(u => u.ProductId == id);
            // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            // Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault() ; 

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);


        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            Product? obj = _productRepo.Get(u => u.ProductId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _productRepo.Remove(obj);
            _productRepo.Save();
            return RedirectToAction("Index");





        }
    }
}

