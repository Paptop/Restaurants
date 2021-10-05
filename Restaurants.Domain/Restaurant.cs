using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required, StringLength(80)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string Location { get; set; }
        [StringLength(800)]
        public string Description { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
