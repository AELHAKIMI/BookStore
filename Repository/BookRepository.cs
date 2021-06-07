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
                new BookModel(){Id = 1, Title="Python", Author= "Ayoub", Description="description for Python book", Category="Programming", Language="English",TotalPages=854},
                new BookModel(){Id = 2, Title="Java", Author= "Abderrahim", Description="description for Java book", Category="Development", Language="French",TotalPages=1024},
                new BookModel(){Id = 3, Title="C#", Author= "Nasser", Description="description for C# book", Category="Programming", Language="English",TotalPages=1080},
                new BookModel(){Id = 4, Title="HTML CSS", Author= "Mohammed", Description="description for HTML&CSS book", Category="Web Design", Language="Hindi",TotalPages=325},
                new BookModel(){Id = 5, Title="PHP", Author= "Khalid", Description="description for PHP book", Category="Web Development", Language="English",TotalPages=814},
            };
        }
    }
}
