using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShopManagementBO;
using MyShopManagementService;

namespace MyShopRazorPages.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService productService;

        public CreateModel(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult OnGet()
        {
            // select box forward data
            ViewData["CategoryId"] = new SelectList(productService.GetCategories(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // select box forward data
            ViewData["CategoryId"] = new SelectList(productService.GetCategories(), "Id", "Name");

            if (!ModelState.IsValid || productService.GetAll() == null || Product == null)
            {
                return Page();
            }

            if (productService.Exist(Product.Id))
            {
                ModelState.AddModelError("Product.Id", "Product ID already exists.");
                return Page();
            }

            productService.Create(Product);

            return RedirectToPage("./Index");
        }
    }
}
