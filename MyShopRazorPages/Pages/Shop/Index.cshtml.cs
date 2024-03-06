using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShopManagementBO;
using MyShopManagementService;

namespace MyShopRazorPages.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly IProductService productService;

        public IndexModel(IProductService productService)
        {
            this.productService = productService;
        }

        public IList<Product> Product { get; set; } = default!;
        public int PageIndex { get; set; }
        public int LastPageIndex { get; set; }

        public async Task OnGetAsync(int pageIndex = 1, int pageSize = 6)
        {
            List<Product> allOfItems = productService.GetAll().ToList();
            //allOfItems.Reverse();

            PageIndex = pageIndex;
            int numberOfItems = allOfItems.Count;
            LastPageIndex = (numberOfItems + pageSize - 1) / pageSize;

            // Perform pagination logic
            Product = allOfItems
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }
    }
}
