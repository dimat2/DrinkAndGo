using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using DrinkAndGo.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DrinkAndGo.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;

        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IDrinkRepository drinkRepository, ShoppingCart shoppingcart)
        {
            _drinkRepository = drinkRepository;
            _shoppingCart = shoppingcart;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            _shoppingCart.ShoppingCartItems = items;

            var shCViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shCViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int drinkId)
        {
            var selectedDrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
            if (selectedDrink != null)
            {
                _shoppingCart.AddToCart(selectedDrink, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int drinkId)
        {
            var selectedDrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
            if (selectedDrink != null)
            {
                _shoppingCart.RemoveFromCart(selectedDrink);
            }

            return RedirectToAction("Index");
        }
    }
}
