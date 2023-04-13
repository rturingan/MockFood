using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MockFood.Data;
using MockFood.Models;

namespace MockFood.Pages.CheckOut
{
    [BindProperties(SupportsGet = true)]
    public class CheckOutModel : PageModel
    {
        public string WingAssort { get; set; }
        public float WingPrice { get; set; }
        public string ImageTitle { get; set; }

    /*    private readonly ApplicationDbContext _context;
        public CheckOutModel(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public void OnGet()     
        {

            if (string.IsNullOrWhiteSpace(WingAssort))
            {
                WingAssort = "Custom";
            }
            if (string.IsNullOrWhiteSpace(ImageTitle))
            {
                ImageTitle = "assorted";
            }

          /*  WingOrder wingOrder = new WingOrder();
            wingOrder.WingAssort = WingAssort;
            wingOrder.BasePrice = WingPrice;

            _context.WingOrders.Add(wingOrder);
            _context.SaveChanges();
                        */
        }
        
    
    }
}
