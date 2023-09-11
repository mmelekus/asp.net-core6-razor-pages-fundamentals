using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;
using WiredBrainCoffeeAdmin.Data.Models;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private IProductRepository _productRepo;
        private IWebHostEnvironment _webEnv;

        public AddProductModel(IProductRepository productRepository, IWebHostEnvironment environment)
        {
            _productRepo = productRepository;
            _webEnv = environment;
        }

        [BindProperty]
        public Product NewProduct { get; set; } = new Product();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) { return Page(); }

            if (NewProduct.Upload is not null)
            {
                NewProduct.ImageFileName = NewProduct.Upload.FileName;

                var file = Path.Combine(_webEnv.ContentRootPath, "wwwroot/images/menu", NewProduct.Upload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await NewProduct.Upload.CopyToAsync(fileStream);
                }
            }

            NewProduct.Created = DateTime.Now;
            _productRepo.Add(NewProduct);

            return RedirectToPage("ViewAllProducts");
        }
    }
}
