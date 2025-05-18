using DataModel;

namespace InMemoryRepo.Interfaces
{
    public interface IProductInMemoryRepo
    {
        Boolean add(ProductDataModel productDataModel);
        List<ProductDataModel> get();
        List<ProductDataModel> get(string color);
    }
}
