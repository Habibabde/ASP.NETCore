using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestCoreApp.Models;

namespace TestCoreApp.Controllers
{
    public class ItemsController : Controller
    {
        public ItemsController(AppDbContext db)
        {
            _db = db;
        }

        private readonly AppDbContext _db;
        public IActionResult Index()
        {
            IEnumerable<Item> itemsList = _db.Items.Include(c => c.Category).ToList();
            return View(itemsList);
        }

        //Get
        public IActionResult New()
        {
            createSelectList();
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {
            if(item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can't equal 100");
            }
            if (ModelState.IsValid)
            {
                _db.Items.Add(item);
                _db.SaveChanges();
                TempData["successData"] = "Item has been successfully";
                return RedirectToAction("Index");

            }
            else
                return View(item);
        }
        public void createSelectList(int selectId=1)
        {
            //    List<Category> categories = new List<Category>()
            //{
            //    new Category(){Id=0, Name="Select Category"},
            //    new Category(){Id=1, Name="Computers"},
            //    new Category(){Id=2, Name="Mobiles"},
            //    new Category(){Id=3, Name="Electric machines"}
            //};

            List<Category> categories= _db.Categories.ToList();
            SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
            ViewBag.CategoryList  = listItems;
        }

        //Get
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id==0)
            {
                return NotFound();
               
                } 
            var item = _db.Items.Find(Id);

            if (item == null)
            {
                return NotFound();
            }
            createSelectList(item.CategoryId);
            return View(item);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can't equal 100");
            }
            if (ModelState.IsValid)
            {
                _db.Items.Update(item);
                _db.SaveChanges();
                TempData["successData"] = "Item has been updated successfully";
                return RedirectToAction("Index");

            }
            else
            {
                return View(item);
            }
        }

        //Get
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }

            createSelectList(item.CategoryId);
            return View(item);
        }
        //Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int? Id)
        {
               var item =_db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
          
                _db.Items.Remove(item);
                _db.SaveChanges();
            TempData["successData"] = "Item has been Deleted successfully";
            return RedirectToAction("Index");

           
        }
    }
}
