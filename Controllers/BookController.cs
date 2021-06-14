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
        [ViewData]
        public string Title { get; set; }
        public BookController(BookRepository bookRepository)
        {
           _bookRepository = bookRepository; 
        }
        public async Task<IActionResult> GetAllBooks()
        {
            Title = "All Books";
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }
        public IActionResult GetTopBooks()
        {

            var data = _bookRepository.GetTopBooks();
            return View(data);
        }
        public async Task<ViewResult> GetBook(int id){
            
            
            var data = await  _bookRepository.GetBookById(id);
            Title = "Book Details " + data.Title;
            return View(data);
        }

        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0){
            ViewBag.bookId = bookId;
            ViewBag.isSuccess = isSuccess;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel){
            if (ModelState.IsValid){
                
            int id = await  _bookRepository.AddNewBook(bookModel);
            if (id > 0){
                return RedirectToAction(nameof(AddNewBook),new { isSuccess = true, bookId = id});
            }  
            }
            return View();
        }
    }
}