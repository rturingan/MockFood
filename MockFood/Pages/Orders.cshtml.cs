using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MockFood.Data;
using MockFood.Models;

namespace MockFood.Pages
{
    public class OrdersModel : PageModel
    {
        public List<WingOrder> WingOrders = new List<WingOrder>();

        private readonly ApplicationDbContext _context;
        public OrdersModel(ApplicationDbContext context)
        {
            _context = context;                
        }

        public List<WingOrder> GetWingOrders() => WingOrders;

        public void OnGet(List<WingOrder> wingOrders, List<WingOrder> _)
        {
            
        }
    }
}
