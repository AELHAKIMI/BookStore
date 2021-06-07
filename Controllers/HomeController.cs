using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookRepository _bookrepository = null;
        public HomeController()
        {
            _bookrepository = new BookRepository();
        }
        public IActionResult Index()
        {
            var data = _bookrepository.GetTopBooks();
            return View(data);
        }
        public ViewResult AboutUs(){
            return View();
        }
        public ViewResult ContactUs(){
            return View();
        }
    }
}