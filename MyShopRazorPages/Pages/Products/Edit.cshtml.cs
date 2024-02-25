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

namespace MyShopRazorPages.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductService productService;

        public EditModel(IProductService productService)
        {
            this.productService = productService;
        }

        [BindProperty]
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
            Product = item;
            ViewData["CategoryId"] = new SelectList(productService.GetCategories(), "Id", "Name");
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
            ViewData["CategoryId"] = new SelectList(productService.GetCategories(), "Id", "Name");
            try
            {
                productService.Update(Product);
            }
            catch
            {
                if (productService.Exist(Product.Id) == false)
                {
                    return NotFound();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
