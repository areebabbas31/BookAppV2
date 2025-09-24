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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        // GET: /<controller>/

        public CategoryController(ICategoryRepository db)
        {

            _categoryRepo=db;
        }
        public IActionResult Index()
        {
            List<Category>? objCategoryList = _categoryRepo.GetAll().ToList();
            //List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }



        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name");
            }

            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category created successfully";
                return View();
            }
            return View();
        }



        public IActionResult Edit(int? id)
        {
          if(id==null || id==0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u=>u.CategoryId==id);
           // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
           // Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault() ; 

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);


        }


        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category updated successfully";
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
            Category? categoryFromDb = _categoryRepo.Get(u => u.CategoryId == id);
            // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            // Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault() ; 

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);


        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            Category? obj = _categoryRepo.Get(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            return RedirectToAction("Index");





        }
    }
}

