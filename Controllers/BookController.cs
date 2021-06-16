using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {   
        private readonly BookRepository _bookRepository =  null;
        private readonly LanguageRepository _languageRepository = null;
        [ViewData]
        public string Title { get; set; }
        public BookController(BookRepository bookRepository, LanguageRepository languageRepository)
        {
           _bookRepository = bookRepository;
           _languageRepository = languageRepository; 
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

        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0){
            // var model = new BookModel(){
            //     Language = "en"
            // };
            
            ViewBag.Language = new SelectList( await _languageRepository.GetLanguages(), "Id", "Name");
            ViewBag.bookId = bookId;
            ViewBag.isSuccess = isSuccess;
            // return View(model);
           
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
            ViewBag.Language = new SelectList( await _languageRepository.GetLanguages(), "Id", "Name");
            return View();
        }


    }
}