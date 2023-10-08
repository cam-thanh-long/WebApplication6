using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication6.Pages
{
    public class InfoShopModel : PageModel
    {
        private readonly ILogger<InfoShopModel> _logger;

        public InfoShopModel(ILogger<InfoShopModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}