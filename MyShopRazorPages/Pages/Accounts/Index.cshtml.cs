using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyShopManagementBO;
using MyShopManagementService;

namespace MyShopRazorPages.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IUserService userService;

		public IndexModel(IUserService userService)
		{
			this.userService = userService;
		}

		public IList<User> User { get; set; } = default!;
		public int PageIndex { get; set; }
		public int LastPageIndex { get; set; }

		public async Task OnGetAsync(int pageIndex = 1, int pageSize = 3)
		{
			List<User> allOfItems = userService.GetAll().Where(item => item.RoleId == 1 || item.RoleId == 2).ToList();
			//allOfItems.Reverse();

			PageIndex = pageIndex;
			int numberOfItems = allOfItems.Count;
			LastPageIndex = (numberOfItems + pageSize - 1) / pageSize;

			// Perform pagination logic
			User = allOfItems
					.Skip((pageIndex - 1) * pageSize)
					.Take(pageSize)
					.ToList();
		}
    }
}
