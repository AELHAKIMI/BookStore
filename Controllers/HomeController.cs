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
        [ViewData]
        public string Title { get; set; }
        public HomeController(BookRepository bookRepository)
        {
            _bookrepository = bookRepository;
        }
        public IActionResult Index()
        {
            Title = "Home";
            var data = _bookrepository.GetTopBooks();
            return View(data);
        }
        public ViewResult AboutUs(){
            Title = "About Us";
            return View();
        }
        public ViewResult ContactUs(){
            Title = "Contact Us";
            return View();
        }
    }
}