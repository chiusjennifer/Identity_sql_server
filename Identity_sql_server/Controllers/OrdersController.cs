using Identity_sql_server.Data;
using Identity_sql_server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity_sql_server.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
        //1.顯示所有餐點
        [HttpGet]
       public async Task<IActionResult> Menu()
        {
            var meals = await _context.Meals.ToListAsync();
            return View(meals);
        }
        //2.加入餐點至訂單
        [HttpPost]
        public async Task<IActionResult> AddToOrder(int mealId, int quantity)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if(meal == null)
            {
                return NotFound();
            }

            var order = await GetOrCreateOrder();

            var orderItem = new Order_items
            {
                OrderId = order.order_id,
                meal_id = meal.meal_id,
                quantity = quantity,
                subtotal = meal.unit_price * quantity
            };
            _context.OrderItems.Add(orderItem);
            order.total_price += orderItem.subtotal;
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderDetails", new { orderId = order.order_id});
         } 
        //3.顯示訂單內容
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Order_Items)
                .ThenInclude(oi => oi.Meal)
                .FirstOrDefaultAsync(o => o.order_id == orderId);

            if (order == null) 
            {
                return NotFound();
            }
            return View(order);
        }
        //4.結算訂單
        [HttpPost]
        public async Task<IActionResult> Checkout(int orderId, string payMethod, string notes)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if(order == null)
            {
                return NotFound();
            }
            order.pay_method = payMethod;
            order.notes = notes;
            order.status = "Pending";
            order.order_time = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction("OrderConfirmation", new { orderId = order.order_id });
        }
        //5.訂單確認頁面
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            return View(order);
        }
        private async Task<Orders> GetOrCreateOrder()
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.status == "Open");
            if(order == null)
            {
                order = new Orders { status = "Open", total_price = 0 };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }
            return order;
        }
    }
}
