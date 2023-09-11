using WiredBrainCoffeeAdmin.Data.Models;

namespace WiredBrainCoffeeAdmin.Data
{
    public interface IProductRepository
    {
        public void Add(Product product);
        public void Update(Product product);
        public void Delete(int id);
        public Product GetById(int id);
        public IList<Product> GetAll();

    }
}
