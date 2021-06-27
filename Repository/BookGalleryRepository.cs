using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookGalleryRepository{
        private readonly BookStoreContext _context = null;
        
      public BookGalleryRepository(BookStoreContext bookStoreContext)
      {
          _context = bookStoreContext;
      }

      public async Task<List<GalleryModel>> GetBookGalleryByBookId(int bookId){
          var bookGallery  = new List<GalleryModel>();
          var bookGalleries = await _context.BookGallery.Where(x => x.BookId == bookId).ToListAsync();
          foreach (var file in bookGalleries)
          {
              bookGallery.Add(new GalleryModel(){
                  Id = file.Id,
                  Name = file.Name,
                  Url = file.Url
              });
          }
          return bookGallery;

      }


    }
    


}