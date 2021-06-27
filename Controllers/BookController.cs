using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {   
        private readonly BookRepository _bookRepository =  null;
        private readonly LanguageRepository _languageRepository = null;
        private readonly BookGalleryRepository _bookGalleryRepository = null;
        [ViewData]
        public string Title { get; set; }
        private readonly  IWebHostEnvironment _webHostEnvironment ;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository , BookGalleryRepository bookGalleryRepository,IWebHostEnvironment webHostEnvironment )
        {
           _bookRepository = bookRepository;
           _languageRepository = languageRepository;
           _bookGalleryRepository  = bookGalleryRepository;
           _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> GetAllBooks()
        {
            Title = "All Books";
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }
    
        public async Task<ViewResult> GetBook(int id){ 

            var data = await  _bookRepository.GetBookById(id);
            data.Gallery = await _bookGalleryRepository.GetBookGalleryByBookId(id);
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
                if (bookModel.CoverPhoto != null){
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl =  await UploadFile(folder, bookModel.CoverPhoto);
                } 
                if(bookModel.GalleryFiles != null){
                    string folder = "books/gallery/";
                    bookModel.Gallery  = new List<GalleryModel>();
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel(){
                            Url = await UploadFile(folder, file),
                            Name = file.FileName
                        };
                        bookModel.Gallery.Add(gallery);                       
                    }

                }
                if(bookModel.BookContent != null){
                    string folderr = "books/content/";
                    bookModel.BookContentUrl = await UploadFile(folderr, bookModel.BookContent);
                }            
            int id = await  _bookRepository.AddNewBook(bookModel);
                if (id > 0){
                    return RedirectToAction(nameof(AddNewBook),new { isSuccess = true, bookId = id});
                }  
            }
            ViewBag.Language = new SelectList( await _languageRepository.GetLanguages(), "Id", "Name");
            return View();
        }
        private async Task<string> UploadFile(string folderPath, IFormFile file){
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }


    }
}