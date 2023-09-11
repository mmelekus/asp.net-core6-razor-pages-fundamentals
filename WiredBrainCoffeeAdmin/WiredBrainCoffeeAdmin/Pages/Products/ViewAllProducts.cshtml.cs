using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;
using WiredBrainCoffeeAdmin.Data.Models;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class ViewAllProductsModel : PageModel
    {
        private IProductRepository _productRepo;

        public IList<Product> Products { get; set; }

        public ViewAllProductsModel(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        public void OnGet()
        {
            Products = _productRepo.GetAll();
        }
    }
}
