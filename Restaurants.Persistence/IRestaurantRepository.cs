using System;
using System.Collections.Generic;
using Restaurants.Domain;


namespace Restaurants.Persistence
{
    public interface IRestaurantRepository
    {
        IEnumerable<Restaurant> GetAll();
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        int SaveChanges();
    }
}
