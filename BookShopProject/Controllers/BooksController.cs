using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookShopProject.DataAccessLayer;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class BooksController : Controller
    {
        private BookShopContext db = new BookShopContext();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author);
            return View(books.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Title,Description,AuthorRefId")] Book book)
        {
            if (ModelState.IsValid)
            {
                Author a = db.Authors.Find(book.AuthorRefId);
                book.Author = a;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorRefId = new SelectList(db.Authors, "AuthorId", "Name", book.AuthorRefId);
            return View(book);
        }
    }
}