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

        public class BillingInfo
        {
            public string billing_address1 { get; set; }
            public string billing_address2 { get; set; }
            public string billing_city { get; set; }
            public string billing_state { get; set; }
            public string billing_country { get; set; }
            public string billing_zip { get; set; }
        }

        public class CustomerInfo
        {
            public string fname { get; set; }
            public string lname { get; set; }
            public string mname { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string mobile { get; set; }
            public string dob { get; set; }
            public string signature { get; set; }
        }

        public class Order
        {
            public string WingAssort { get; set; }
            public string ImageTitle { get; set; }
            public int quantity { get; set; }
            public float WingPrice { get; set; }
            public float totalprice { get; set; }
        }

        public class OrderDetails
        {
            public List<Order> orders { get; set; }
            public string subtotalprice { get; set; }
            public string shippingprice { get; set; }
            public string discountamount { get; set; }
            public string totalorderamount { get; set; }
        }

        public class Root
        {
            public Transaction transaction { get; set; }
            public BillingInfo billing_info { get; set; }
            public ShippingInfo shipping_info { get; set; }
            public CustomerInfo customer_info { get; set; }
            public OrderDetails order_details { get; set; }
        }

        public class ShippingInfo
        {
            public string shipping_address1 { get; set; }
            public string shipping_address2 { get; set; }
            public string shipping_city { get; set; }
            public string shipping_state { get; set; }
            public string shipping_country { get; set; }
            public string shipping_zip { get; set; }
        }

        public class Transaction
        {
            public string request_id { get; set; }
            public string notification_url { get; set; }
            public string response_url { get; set; }
            public string cancel_url { get; set; }
            public string pchannel { get; set; }
            public string payment_notification_status { get; set; }
            public string payment_notification_channel { get; set; }
            public string amount { get; set; }
            public string currency { get; set; }
            public string trx_type { get; set; }
            public string signature { get; set; }
        }

        private readonly ApplicationDbContext _context;
        public CheckOutModel(ApplicationDbContext context)
        {
            _context = context;
        }
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

            WingOrder wingOrder = new WingOrder();
            wingOrder.WingAssort = WingAssort;
            wingOrder.BasePrice = WingPrice;

            _context.WingOrders.Add(wingOrder);
            _context.SaveChanges();
                        
        }
        
    
    }
}
