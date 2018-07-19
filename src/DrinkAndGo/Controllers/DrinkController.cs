using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using DrinkAndGo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkAndGo.Controllers
{
    public class DrinkController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDrinkRepository _drinkRepository;

        public DrinkController(ICategoryRepository categoryRepository, IDrinkRepository drinkRepository)
        {
            _categoryRepository = categoryRepository;
            _drinkRepository = drinkRepository;
        }

        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Drink> drinks;

            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                drinks = _drinkRepository.Drinks.OrderBy(n => n.DrinkId);
                currentCategory = "All drinks";
            }
            else
            {
                if (string.Equals("Alcoholic", _category, StringComparison.OrdinalIgnoreCase))
                {
                    drinks = _drinkRepository.Drinks.Where(p => p.Category.CategoryName.Equals("Alcoholic")).OrderBy(p => p.Name);
                }
                else
                {
                    drinks = _drinkRepository.Drinks.Where(p => p.Category.CategoryName.Equals("Non-alcoholic")).OrderBy(p => p.Name);
                }

                currentCategory = _category;
            }

            var drinkListViewModel = new DrinkListViewModel
            {
                Drinks = drinks,
                CurrentCategory = currentCategory
            };

            return View(drinkListViewModel);
        }

        public ViewResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Drink> drinks;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                drinks = _drinkRepository.Drinks.OrderBy(p => p.DrinkId);
            }
            else
            {
                drinks = _drinkRepository.Drinks.Where(p => p.Name.ToLower().Contains(_searchString.ToLower()));
            }
            
            return View("~/Views/Drink/List.cshtml", new DrinkListViewModel
            {
                Drinks = drinks,
                CurrentCategory = "All drinks"
            });
        }
    }
}
