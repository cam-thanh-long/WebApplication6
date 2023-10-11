using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication6.Pages
{
    public class CartModel : PageModel
    {
        public List<ShowProduct> ShoppingCart { get; set; } = new List<ShowProduct>();


        public IActionResult OnPostInitializeCart([FromBody] List<ShowProduct> cart)
        {
            // Xử lý dữ liệu giỏ hàng từ dữ liệu POST
            ShoppingCart = cart;

            return new JsonResult("Success");
        }

        public void OnGet()
        {
        }



    }


}
