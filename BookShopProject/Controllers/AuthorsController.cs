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
    public class AuthorsController : Controller
    {
        private BookShopContext db = new BookShopContext();
    }
}
