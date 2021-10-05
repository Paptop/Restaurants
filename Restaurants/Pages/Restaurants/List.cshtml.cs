using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurants.Domain;
using Restaurants.Persistence;

namespace Restaurants.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private IRestaurantRepository _restaurantRepo { get; set; }

        //Model
        public IEnumerable<Restaurant> Restaurants { get; set; }

        //Automatic data binding
        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public ListModel(IRestaurantRepository data)
        {
            _restaurantRepo = data;
        }

        public void OnGet()
        {
            Restaurants = SearchTerm == null ? _restaurantRepo.GetAll() :
                          _restaurantRepo.GetRestaurantsByName(SearchTerm);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int restaurantId)
        {
            /*
            Restaurant res = null;
            if (!_restaurantRepo.TryGetById(restaurantId, ref res))
            {
                return;
            }*/
            //_restaurantRepo.Delete(res);
            TempData["Title"] = _restaurantRepo.GetById(restaurantId).Name;
            return Page();
        }
    }
}
