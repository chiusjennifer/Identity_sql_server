using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity_sql_server.Models
{
    public class Order_items
    {
        [Key]
        public int OrderItemId { get; set; }
        [ForeignKey("Orders")]
        public int OrderId { get; set; }
        [ForeignKey("Meals")]
        public int meal_id { get; set; }
        public int quantity {  get; set; }
        public decimal subtotal { get; set; }

        // 導航屬性
        public Orders Order { get; set; }
        public Meals Meal { get; set; }
    }
}
