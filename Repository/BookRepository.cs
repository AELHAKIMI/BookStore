using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository{
        private readonly BookStoreContext _context = null;
        
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook(BookModel model){
            var newBook = new Books(){
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                Category = model.Category,
                LanguageId = model.LanguageId, 
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CoverImageUrl = model.CoverImageUrl,
                BookContentUrl = model.BookContentUrl,
                UpdatedOn = DateTime.UtcNow
            };
            newBook.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery(){
                    Url = file.Url,
                    Name = file.Name
                });
            }
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks(){
            var  books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if(allbooks?.Any() == true){
                foreach(var book in allbooks){
                    books.Add(new BookModel(){
                        Id = book.Id,
                        Author = book.Author,
                        Description = book.Description,
                        LanguageId = book.LanguageId,
                        // Language = book.Language.Name,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        CoverImageUrl = book.CoverImageUrl,
                        Category = book.Category
                    });
                }
            }
            return books;
        }
        public async Task<List<BookModel>> GetTopBooksAsync(int count){
            return await _context.Books.Select(book => new BookModel(){
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl

            }).Take(count).ToListAsync();
        }        
        public async  Task<BookModel> GetBookById(int id){
            return await _context.Books.Where(x => x.Id == id).Select(book => new BookModel(){
                        Id = book.Id,
                        Author = book.Author,
                        Description = book.Description,
                        LanguageId = book.LanguageId,
                        Language = book.Language.Name,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        CoverImageUrl = book.CoverImageUrl,
                        Category = book.Category,
                        BookContentUrl = book.BookContentUrl                        
            }).FirstOrDefaultAsync();
        }
    }
}
