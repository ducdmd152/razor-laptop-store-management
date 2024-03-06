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
    public class DetailsModel : PageModel
    {
        private readonly IUserService userService;

        public DetailsModel(IUserService userService)
        {
            this.userService = userService;
        }

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
            else
            {
                User = item;
            }
            return Page();
        }
    }
}
