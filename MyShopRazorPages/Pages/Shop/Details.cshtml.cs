using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyShopManagementBO;
using MyShopManagementService;

namespace MyShopRazorPages.Pages.Shop
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService productService;

        public DetailsModel(IProductService productService)
        {
            this.productService = productService;
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || productService.GetAll() == null)
            {
                return NotFound();
            }
            if (!int.TryParse(id, out int productId))
            {
                return NotFound();
            }

            var item = productService.Get(productId);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                Product = item;
            }
            return Page();
        }
    }
}
