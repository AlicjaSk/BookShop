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
    public class PurchasesController : Controller
    {
        private BookShopContext db = new BookShopContext();


        protected void SaveBookInfoInViewBag(Book book)
        {
            ViewBag.bookTitle = book.Title;
            ViewBag.description = book.Description;
            ViewBag.bookId = book.BookId;
        }

        public ActionResult Statictics()
        {
            // SQL version: 
            // 	select top 5 Books.Title, Count(Books.Title) as cnt from Purchases inner join Books on Books.BookId=Purchases.BookRefId group by Books.Title order by cnt desc
            var theMostPopularBooks = (from purchase in db.Purchases
                                       join book in db.Books on purchase.BookRefId equals book.BookId
                                       group purchase by book.Title into number
                                       let count = number.Count()
                                       orderby count descending
                                       select new { bookTitle = number.Key, BooksCount = number.Count() }).Take(5);
            List<string> titles = new List<string>();
            foreach(var b in theMostPopularBooks)
            {
                titles.Add(b.bookTitle);
            }

            return View(titles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyBook([Bind(Include = "ClientId,Name,Surname,Street,NumberStreet,City")] Client client)
        {
            int bookId = Int32.Parse(Request.Form["bookId"]);
            Book book = db.Books.Find(bookId);
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);

                // Register purchase of specific book by specific client
                Purchase p = new Purchase(book, client);
                db.Purchases.Add(p);

                book.NumberInWarehouse -= 1;

                db.SaveChanges();
                
                Author a = db.Authors.Find(book.AuthorRefId);
                ViewBag.authorFullName = a.Name + " " + a.Surname;
                
                return View("ConfirmationOfPurchase", p);
            }
            this.SaveBookInfoInViewBag(book);
            return View(client);
        }

        public ActionResult BuyBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);

            if (book == null)
            {
                return HttpNotFound();
            }
            this.SaveBookInfoInViewBag(book);
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
