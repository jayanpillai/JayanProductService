using DataModel;
using InMemoryRepo.Interfaces;
using System.Drawing;
namespace InMemoryRepo.ConcreteClasses
{
    public class ProductMemoryRepo : IProductInMemoryRepo
    {
        private List<ProductDataModel> _products;

        public ProductMemoryRepo()
        {
            _products = new List<ProductDataModel>();
         }

        public Boolean add(ProductDataModel product)
        {
             _products.Add(product);
            if (_products.Contains(product))
            {
                return true;
            }
            return false;
        }

        public List<ProductDataModel> get()
        {
            return _products;
        }

        public List<ProductDataModel> get(string colorfilter)
        {
            return _products.Where(x=>x.colour.Equals(colorfilter)).ToList();
        }
    }
}
