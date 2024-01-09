using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objectcategorylist = _db.Categories.ToList();
            return View(objectcategorylist);
        }

        //GET
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //this is for security porpuses
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DifferenciateError", "The Display Order Can Not Exactly Match The Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Successful"] = "Category Created Successfully";
                return RedirectToAction("Index");

            }
            return View(obj);
            
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var CategoryFromDb = _db.Categories.Find(id);
            //var CategoryFromDbFirst = _db.Categories.SingleOrDefault(c => c.Id == id);

            //var CategoryFromDbSingle = _db.Categories.FirstOrDefault(u => u.Id == id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }

            return View(CategoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //this is for security porpuses
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DifferenciateError", "The Display Order Can Not Exactly Match The Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Successful"] = "Category Updated Successfully";
                return RedirectToAction("Index");

            }
            return View(obj);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var CategoryFromDb = _db.Categories.Find(id);

            if(CategoryFromDb == null)
            {
                return NotFound();
            }
            else
            {
                return View(CategoryFromDb);
            }

        }


        [HttpPost]
        public IActionResult Delete(Category obj)
        {
            _db.Categories.Remove(obj);
            _db.SaveChanges(true);
            TempData["Successful"] = "Category Removed Successfully";
            return RedirectToAction("Index");
        }
        
    }
}
