using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            :base (options)
        {
            
        }
        public DbSet<Books> Books {get; set;}

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseMySQL("Server=localhost;Database=BookStore;Uid=root;Pwd=Mysql@2021;");
        //     base.OnConfiguring(optionsBuilder);
        // }
    }
}