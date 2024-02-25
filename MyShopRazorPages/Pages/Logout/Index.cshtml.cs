using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyShopRazorPages.Pages.Logout
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
			HttpContext.Session.Clear();
			return Redirect("/Index");
		}
    }
}
