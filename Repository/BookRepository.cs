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
        public BookModel GetBookById(int id){
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string title , string authorename){
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorename)).ToList();
        }

        private List<BookModel> DataSource(){
            return new List<BookModel>(){
                new BookModel(){Id = 1, Title="Python", Author= "Ayoub"},
                new BookModel(){Id = 2, Title="Java", Author= "Abderrahim"},
                new BookModel(){Id = 3, Title="C#", Author= "Nasser"},
                new BookModel(){Id = 4, Title="HTML CSS", Author= "Mohammed"},
                new BookModel(){Id = 5, Title="PHP", Author= "Khalid"},
            };
        }
    }
}