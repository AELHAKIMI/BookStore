using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class LanguageController : Controller
    {           
        private readonly LanguageRepository _languageRepository = null;
        public LanguageController( LanguageRepository languageRepository)
        {           
           _languageRepository = languageRepository; 
        }
        public  ViewResult AddLanguage(bool isSuccess = false, int languageId= 0){
            
            ViewBag.isSuccess = isSuccess;
            ViewBag.LanguageId = languageId;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddLanguage(LanguageModel model){
            if (ModelState.IsValid){
                int id = await _languageRepository.AddLanguage(model);
                if(id > 0){
                    return RedirectToAction(nameof(AddLanguage), new { isSuccess = true, languageId = id});
                }
            }
            return View();
        }
    }
}