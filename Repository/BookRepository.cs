using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repository
{
    public class BookRepository{
        public List<BookModel> GetAllBooks(){
            return DataSource();
        }
        public IEnumerable<BookModel> GetTopBooks(){
            return DataSource().OrderBy(x => x.Id).Take(3);
        }
        public BookModel GetBookById(int id){
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string title , string authorename){
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorename)).ToList();
        }

        private List<BookModel> DataSource(){
            return new List<BookModel>(){
                new BookModel(){Id = 1, Title="Python", Author= "Ayoub", Description="description for Python book"},
                new BookModel(){Id = 2, Title="Java", Author= "Abderrahim", Description="description for Java book"},
                new BookModel(){Id = 3, Title="C#", Author= "Nasser", Description="description for C# book"},
                new BookModel(){Id = 4, Title="HTML CSS", Author= "Mohammed", Description="description for HTML&CSS book"},
                new BookModel(){Id = 5, Title="PHP", Author= "Khalid", Description="description for PHP book"},
            };
        }
    }
}
