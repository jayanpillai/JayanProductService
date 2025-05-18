
using DataModel;
using DataService.Interfaces;
using InMemoryRepo;
using InMemoryRepo.ConcreteClasses;
using InMemoryRepo.Interfaces;

namespace DataService.ConcreteClasses
{
    public class ProductService : IProductService
    {
        private readonly IProductInMemoryRepo _productRepo;

        public ProductService(IProductInMemoryRepo productInMemory)
        {
            _productRepo = productInMemory;
        }


        public Boolean add(ProductDataModel productDataModel)
        {
            return _productRepo.add(productDataModel);
        }

        public List<ProductDataModel> getAll()
        {
            return _productRepo.get();
        }

        public List<ProductDataModel> get(string color)
        {
            return _productRepo.get(color);
        }
    }


}