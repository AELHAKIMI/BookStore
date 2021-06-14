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
                Language = model.Language, 
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow
            };
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
                        Language = book.Language,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        Category = book.Category

                    });
                }

            }
            return books;
        }
        public IEnumerable<BookModel> GetTopBooks(){
            return DataSource().OrderBy(x => x.Id).Take(3);
        }
        public async  Task<BookModel> GetBookById(int id){
            
            var book = await _context.Books.FindAsync(id);
            if(book != null){
                var bookDeatails = new BookModel(){
                        Id = book.Id,
                        Author = book.Author,
                        Description = book.Description,
                        Language = book.Language,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        Category = book.Category
                };
                return bookDeatails;
            }
            return null;
            
        }
        public List<BookModel> SearchBook(string title , string authorename){
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorename)).ToList();
        }

        private List<BookModel> DataSource(){
            return new List<BookModel>(){
                new BookModel(){Id = 1, Title="Python", Author= "Ayoub", Description="description for Python book", Category="Programming", Language="English",TotalPages=854},
                new BookModel(){Id = 2, Title="Java", Author= "Abderrahim", Description="description for Java book", Category="Development", Language="French",TotalPages=1024},
                new BookModel(){Id = 3, Title="C#", Author= "Nasser", Description="description for C# book", Category="Programming", Language="English",TotalPages=1080},
                new BookModel(){Id = 4, Title="HTML CSS", Author= "Mohammed", Description="description for HTML&CSS book", Category="Web Design", Language="Hindi",TotalPages=325},
                new BookModel(){Id = 5, Title="PHP", Author= "Khalid", Description="description for PHP book", Category="Web Development", Language="English",TotalPages=814},
            };
        }
    }
}
