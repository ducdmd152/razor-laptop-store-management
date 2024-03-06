using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShopManagementBO;
using MyShopManagementService;

namespace MyShopRazorPages.Pages.Accounts
{
	public class EditModel : PageModel
	{
		private readonly IUserService userService;

		public EditModel(IUserService userService)
		{
			this.userService = userService;
		}

		[BindProperty]
		public User User { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (id == null || userService.GetAll() == null)
			{
				return NotFound();
			}

			var item = userService.Get(id);

			if (item == null)
			{
				return NotFound();
			}
			User = item;
			ViewData["RoleId"] = new SelectList(userService.GetRoles().Where(item => item.Id != 3), "Id", "Name");
			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			ViewData["RoleId"] = new SelectList(userService.GetRoles().Where(item => item.Id != 3), "Id", "Name");
			try
			{
				userService.Update(User);
			}
			catch
			{
				if (userService.Exist(User.Email) == false)
				{
					return NotFound();
				}
			}

			return RedirectToPage("./Index");
		}
	}
}
