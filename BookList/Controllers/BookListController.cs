using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookList.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookList.Controllers
{
    public class BookListController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookListController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Book> objList = _db.Book;
            return View(objList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj)
        {
            if (ModelState.IsValid)
            {
                _db.Book.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET - Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Book.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
    }
}
