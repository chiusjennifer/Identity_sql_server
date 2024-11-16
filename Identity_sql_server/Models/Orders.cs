using System.ComponentModel.DataAnnotations;

namespace Identity_sql_server.Models
{
    public class Orders
    {
        [Key]
        public int order_id { get; set; }
        public int customer_id {  get; set; }
        public DateTime order_time { get; set; }
        public Decimal total_price { get; set; }
        public string pay_method { get; set; }
        public int card_id { get; set; }
        public string status { get; set; }
        public string notes { get; set; }

        public ICollection<Order_items> Order_Items { get; set; }
    }
}
