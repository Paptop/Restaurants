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
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantRepository _restaurantRepo;
        private Restaurant _resToDelete;

        public Restaurant Restaurant {
            get { return _resToDelete; }
            set { _resToDelete = value; }
        }

        public DeleteModel(IRestaurantRepository data)
        {
            _restaurantRepo = data;
        }


        public IActionResult OnGet(int restaurantId)
        {
            if (!_restaurantRepo.TryGetById(restaurantId, ref _resToDelete))
            {
                return RedirectToPage("./NotFound");
            }
            Restaurant = _resToDelete;
            return Page();
        }

        public IActionResult OnPost(int restaurantId)
        {
            if (!_restaurantRepo.TryGetById(restaurantId, ref _resToDelete))
            {
                return RedirectToPage("./NotFound");
            }

            Restaurant = _resToDelete;

            _restaurantRepo.Delete(_resToDelete);
            return RedirectToPage("./List");
        }
    }
}
