using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MockFood.Models;

namespace MockFood.Pages.Forms
{
    public class FlavorModel : PageModel
    {
        [BindProperty]
        public WingModel Sauce { get; set; }
        public float WingPrice { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            WingPrice = Sauce.BasePrice;

            if (Sauce.ClassicBuffalo) WingPrice += 5;
            if (Sauce.JalapChiliCheese) WingPrice += 5;
            if (Sauce.Ranch) WingPrice += 5;
            if (Sauce.BuffRanch) WingPrice += 5;
            if (Sauce.SweetSoy) WingPrice += 5;
            if (Sauce.SaltPepper) WingPrice += 0;
            if (Sauce.HoneyBasil) WingPrice += 5;
            if (Sauce.SrirachaHoney) WingPrice += 5;

            return RedirectToPage("/CheckOut/CheckOut", new { Sauce.WingAssort, WingPrice });
        }
    }
}   
