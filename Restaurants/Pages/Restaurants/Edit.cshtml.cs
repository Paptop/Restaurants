using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurants.Domain;
using Restaurants.Persistence;

namespace Restaurants.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantRepository _restaurantRepo;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Restaurant RestaurantData { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantRepository data,
                         IHtmlHelper htmlHelper)
        {
            _restaurantRepo = data;
            _htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int restaurantId)
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
            RestaurantData = _restaurantRepo.GetById(restaurantId);

            if (RestaurantData == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
            RestaurantData = _restaurantRepo.Update(RestaurantData);
            _restaurantRepo.SaveChanges();
            return Page();
        }
    }
}
