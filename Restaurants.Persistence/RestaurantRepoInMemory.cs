using System.Linq;
using System.Collections.Generic;
using Restaurants.Domain;
using System;

namespace Restaurants.Persistence
{
    public class RestaurantRepoInMemory : IRestaurantRepository
    {
        private List<Restaurant> restaurants;

        public RestaurantRepoInMemory()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Cili Pizza", Location="Vilnius, Akropolis", Cuisine=CuisineType.Italian,
                Description="ČILI PIZZA - vienas didžiausių picerijų tinklų Baltijos šalyse, veikiantis franšizės pagrindu. Tyrimų duomenimis, daugiau nei trečdalis Lietuvos gyventojų ČILI PIZZA įvardina kaip mėgstamiausią restoranų tinklą, net 6 iš 10 gyventojų ČILI picerijose lankosi bent kartą per mėnesį, o ČILI žinomumas siekia 95 proc. Vykdome picerijų plėtrą ir siūlome naujas galimybes Jūsų esamam ar naujam verslui – įsigykite ČILI PIZZA franšizę, atstovaukite vieną stipriausių ir gerausiai žinomų prekės ženklų Baltijos šalyse bei pasinaudokite mūsų sukaupta patirtimi. Taip pat siūlome įsigyti ČILI TAKE AWAY bei SOYA(soya.lt) franšizes."},
                new Restaurant { Id = 2, Name = "Manami", Location="Vilnius, CUP", Cuisine=CuisineType.Japanese,
                Description="MANAMI restorane egzotiška kelionė po neragautą Aziją atneš daugiau patirčių ir įspūdžių nei bet kur kitur, nes tik čia kokybiškai atstovaujamos visos ..."},
                new Restaurant { Id = 3, Name = "Da Antonio", Location="Vilnius, Vilniaus g. 23", Cuisine=CuisineType.Italian,
                Description="„Da Antonio“ restoranas, atidarytas Vilniaus senamiestyje 1997 metais. Kadangi restoranas „Da Antonio“ įsikūręs Vilniaus senamiestyje, kuriant interjerą stengtasi išlaikyti pastatui būdingus ornamentus, faktūras ir spalvinę gamą. Švelnūs gelsvi sienų tonai pagyvinti vyšnių spalvos klasikiniais minkštasuoliais. Kiekviena restorano niša puošta italų dailininkų reprodukcijomis, vazomis, antikvariniais laikrodžiais. Restorano prieigos išsiskiria simbolinėmis Antikos kolonomis ir frontonu."},
                new Restaurant { Id = 4, Name = "AL Pastor", Location="Vilnius, A. Smetonos g. 5", Cuisine=CuisineType.Mexican,
                Description="Tasty Mexican Restaurant In Vilnius"},
                new Restaurant { Id = 5, Name = "Bernelių Užeiga", Location="Vilnius, Gedimino pr. 19", Cuisine=CuisineType.Lithuanian,
                Description="„Bernelių užeiga“ jau 20 metų puoselėja lietuvišką virtuvę, dėl kurios kasdien grįžta ištikimi klientai ir užsuka Lietuvą lankantys turistai. Pastariesiems didžiulį įspūdį daro ne tik tautinio paveldo sertifikatą turintys, autentiški 43 valgiaraštyje esantys patiekalai. Juos žavi ir „Bernelių užeigos“ padavėjų"},
            };
        }

        public Restaurant Create(Restaurant newRestaurant)
        {
            if(newRestaurant == null)
            {
                return null;
            }

            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            restaurants.Add(newRestaurant);
            return newRestaurant;
        }

        public bool Delete(Restaurant restaurant)
        {
            return restaurants.Remove(restaurant);
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

        public int SaveChanges()
        {
            //InMemory database
            return 0;
        }

        public bool TryGetById(int id, ref Restaurant res)
        {
            res = GetById(id);
            return res != null;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            if(updatedRestaurant == null)
            {
                return null;
            }

            Restaurant restaurant = null;
            if(!TryGetById(updatedRestaurant.Id, ref restaurant))
            {
                return null;
            }

            //TODO: move to separate method
            restaurant.Cuisine = updatedRestaurant.Cuisine;
            restaurant.Location = updatedRestaurant.Location;
            restaurant.Name = updatedRestaurant.Name;
            restaurant.Description = updatedRestaurant.Description;

            return restaurant;
        }
    }
}
