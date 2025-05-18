using DataModel;
using InMemoryRepo;

namespace DataService.Interfaces
{
    public interface IProductService
    {
        Boolean add(ProductDataModel productDataModel);
        List<ProductDataModel> getAll();
        List<ProductDataModel> get(string color);
    }
}
