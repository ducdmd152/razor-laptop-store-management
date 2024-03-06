using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShopManagementBO;
using MyShopRazorPages.Data;
using Newtonsoft.Json;

namespace MyShopRazorPages.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public IActionResult OnGet()
		{
            var userString = HttpContext.Session.GetString("CREDENTIAL");
            var user = userString != null ? JsonConvert.DeserializeObject<User>(userString) : null;

			if (user != null && user.RoleId == (int)Roles.CUSTOMER)
			{
                return RedirectToPage("/Shop/Index");
            }

			return Page();
        }
	}
}