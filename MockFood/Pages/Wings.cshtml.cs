using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MockFood.Models;

namespace MockFood.Pages
{
    public class WingsModel : PageModel
    {
        public List<WingModel> fakeWingDB = new List<WingModel>()
        {
            new WingModel()
            {
            ImageTitle = "Cbuffalo",
            WingAssort = "Buffalo Ranch",
            BasePrice = 350,
            BuffRanch = true,
            WingPrice = 355
            },
            new WingModel()
            {
            ImageTitle = "Cbuffalo",
            WingAssort = "Classic Buffalo",
            BasePrice = 350,
            ClassicBuffalo = true,
            WingPrice = 355
            },
             new WingModel()
            {
            ImageTitle = "Cbuffalo",
            WingAssort = "Ranch",
            BasePrice = 350,
            Ranch = true,
            WingPrice = 355
            },


        };

        public void OnGet()
        {
        }
    }
}
    