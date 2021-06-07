using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {   
        private readonly BookRepository _bookRepository =  null;
        public BookController()
        {
           _bookRepository = new BookRepository(); 
        }
        public IActionResult GetAllBooks()
        {
            var data = _bookRepository.GetAllBooks();
            return View(data);
        }
        public IActionResult GetTopBooks()
        {
            var data = _bookRepository.GetTopBooks();
            return View(data);
        }
        public IActionResult GetBook(int id){
            var data =  _bookRepository.GetBookById(id);
            return View(data);
        }

        public List<BookModel> SearchBook(string authorName, string bookName){
            return _bookRepository.SearchBook(bookName, authorName);
        }
    }
}