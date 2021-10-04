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
    public class DetailModel : PageModel
    {
        public Restaurant RestaurantData { get; set; }

        private IRestaurantRepository _restaurantRepo;

        public DetailModel(IRestaurantRepository data)
        {
            _restaurantRepo = data;
        }

        public IActionResult OnGet(int restaurantId)
        {
            RestaurantData = _restaurantRepo.GetById(restaurantId);
            if(RestaurantData == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}
