using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShopManagementBO;
using MyShopManagementService;

namespace MyShopRazorPages.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly IUserService userService;

        public CreateModel(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult OnGet()
        {
            // select box forward data
            ViewData["RoleId"] = new SelectList(userService.GetRoles().Where(item => item.Id != 3), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // select box forward data
            ViewData["RoleId"] = new SelectList(userService.GetRoles().Where(item => item.Id != 3), "Id", "Name");

            if (!ModelState.IsValid || userService.GetAll() == null || User == null)
            {
                return Page();
            }

            if (userService.Exist(User.Email))
            {
                ModelState.AddModelError("User.Email", "Email already exists.");
                return Page();
            }

            userService.Create(User);

            return RedirectToPage("./Index");
        }
    }
}
