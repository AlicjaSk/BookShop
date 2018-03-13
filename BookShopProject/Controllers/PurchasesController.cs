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

        // GET: Purchases
        public ActionResult Index()
        {
            var purchases = db.Purchases.Include(p => p.Book).Include(p => p.Client);
            return View(purchases.ToList());
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        public ActionResult Statictics()
        {


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


                //   return View(theMostPopularBooks.Select(x => db.Books.Find(x.bookId)));
        }
        //List<Book> books = theMostPopularBooks.ToList().M;
        //            foreach (var n in theMostPopularBooks)
        //{
        //Book b = db.Books.Find(int.Parse(n.bookId.ToString()));
        //books.Add(b);   
        //   tmp += n.bookId;

        //}
        //return tmp;

        //return View(books);



        // GET: Purchases/Create
        public ActionResult Create()
        {
            ViewBag.BookRefId = new SelectList(db.Books, "BookId", "Title");
            ViewBag.ClientRefId = new SelectList(db.Clients, "ClientId", "Name");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseId,ClientRefId,BookRefId")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookRefId = new SelectList(db.Books, "BookId", "Title", purchase.BookRefId);
            ViewBag.ClientRefId = new SelectList(db.Clients, "ClientId", "Name", purchase.ClientRefId);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookRefId = new SelectList(db.Books, "BookId", "Title", purchase.BookRefId);
            ViewBag.ClientRefId = new SelectList(db.Clients, "ClientId", "Name", purchase.ClientRefId);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseId,ClientRefId,BookRefId")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookRefId = new SelectList(db.Books, "BookId", "Title", purchase.BookRefId);
            ViewBag.ClientRefId = new SelectList(db.Clients, "ClientId", "Name", purchase.ClientRefId);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
            db.SaveChanges();
            return RedirectToAction("Index");
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
