using System.Linq;
using System.Collections.Generic;
using Restaurants.Domain;

namespace Restaurants.Persistence
{
    public class RestaurantRepoInMemory : IRestaurantRepository
    {
        private List<Restaurant> restaurants;

        public RestaurantRepoInMemory()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Cili Pizza", Location="Vilnius, Akropolis", Cuisine=CuisineType.Italian},
                new Restaurant { Id = 2, Name = "Manami", Location="Vilnius, CUP", Cuisine=CuisineType.Japanese},
                new Restaurant { Id = 3, Name = "Da Antonio", Location="Vilnius, Vilniaus g. 23", Cuisine=CuisineType.Italian},
                new Restaurant { Id = 4, Name = "AL Pastor", Location="Vilnius, A. Smetonos g. 5", Cuisine=CuisineType.Mexican},
                new Restaurant { Id = 5, Name = "Bernelių Užeiga", Location="Vilnius, Gedimino pr. 19", Cuisine=CuisineType.Lithuanian},
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;

        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }
    }
}
