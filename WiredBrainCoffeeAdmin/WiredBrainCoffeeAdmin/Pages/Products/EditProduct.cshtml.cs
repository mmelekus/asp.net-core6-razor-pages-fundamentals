using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data.Models;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class EditProductModel : PageModel
    {
        private IProductRepository _productRepo;
        private IWebHostEnvironment _webEnv;

        public EditProductModel(IProductRepository productRepository, IWebHostEnvironment environment)
        {
            _productRepo = productRepository;
            _webEnv = environment;
        }

        [FromRoute]
        public int Id { get; set; }

        [BindProperty]
        public Product EditProduct { get; set; }

        public void OnGet()
        {
            EditProduct = this._productRepo.GetById(Id);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            if (!ModelState.IsValid)
            { return Page(); }

            if (EditProduct.Upload is not null)
            {
                EditProduct.ImageFileName = EditProduct.Upload.FileName;

                var file = Path.Combine(_webEnv.ContentRootPath, "wwwroot/images/menu", EditProduct.Upload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await EditProduct.Upload.CopyToAsync(fileStream);
                }
            }

            EditProduct.Created = DateTime.Now;
            EditProduct.Id = Id;
            _productRepo.Update(EditProduct);

            return RedirectToPage("ViewAllProducts");
        }

        public IActionResult OnPostDelete()
        {
            _productRepo.Delete(Id);
            return RedirectToPage("ViewAllProducts");
        }
    }
}
