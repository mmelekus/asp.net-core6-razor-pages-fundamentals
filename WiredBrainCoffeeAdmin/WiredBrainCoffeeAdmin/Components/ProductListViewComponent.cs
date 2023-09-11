using Microsoft.AspNetCore.Mvc;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Components
{
    public class ProductListViewComponent : ViewComponent
    {
        private IProductRepository _productRepo;

        public ProductListViewComponent(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        public IViewComponentResult Invoke(int count)
        {
            var items = _productRepo.GetAll();
            if(count > 0)
            {
                return View(items.Take(count).ToList());
            }
            return View(items);
        }
    }
}
