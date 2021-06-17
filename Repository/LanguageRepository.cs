using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class LanguageRepository{
        private readonly BookStoreContext _context = null;
        
        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageModel>> GetLanguages(){
            return  await _context.Language.Select(x => new LanguageModel(){
                Id = x.Id,
                Name= x.Name,
                Description = x.Description


            }).ToListAsync();
        }

        public async Task<int> AddLanguage(LanguageModel model){
            var newLanguage =  new Language(){
                Name = model.Name,
                Description = model.Description
            };
            await _context.Language.AddAsync(newLanguage);
            await _context.SaveChangesAsync();
            return newLanguage.Id;
            
        }


    }
    


}