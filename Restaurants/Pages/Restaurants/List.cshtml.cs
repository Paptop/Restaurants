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

        public ListModel(IRestaurantRepository data)
        {
            _restaurantRepo = data;
        }

        public void OnGet(string searchTerm)
        {
            Restaurants = searchTerm == null ? _restaurantRepo.GetAll() :
                          _restaurantRepo.GetRestaurantsByName(searchTerm);
        }
    }
}
