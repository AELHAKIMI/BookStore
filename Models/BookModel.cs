using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStore.Data;
using Microsoft.AspNetCore.Http;

namespace BookStore.Models
{
    public class BookModel{
        public int Id { get; set; }


        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage="Please enter the title of your book")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Please enter th author name ")]
        public string Author { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage ="Please Choose the language of your book")]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage ="Please enter the total pages")]
        [Display(Name ="Total Pages")]
        public int? TotalPages { get; set; }
        [Required]
        public IFormFile CoverPhoto {get; set;}
       

    }
    
}