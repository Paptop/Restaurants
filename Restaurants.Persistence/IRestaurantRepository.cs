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
        bool TryGetById(int id, ref Restaurant res);

        bool Delete(Restaurant restaurant);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Create(Restaurant newRestaurant);
        int SaveChanges();
    }
}
