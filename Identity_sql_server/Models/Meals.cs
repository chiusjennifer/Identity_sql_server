using System.ComponentModel.DataAnnotations;

namespace Identity_sql_server.Models
{
    public class Meals
    {
        [Key]
        public int meal_id { get; set; }
        public string category { get; set; }
        public string dish_name { get; set; }
        public decimal unit_price { get; set; }
    }
}
