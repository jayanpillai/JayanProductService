using DataModel;
using InMemoryRepo;
using InMemoryRepo.ConcreteClasses;
using InMemoryRepo.Interfaces;
using Moq;
using System.Drawing;


namespace TestService
{
    public class TestProductsInMemoryCreation
    {
 
        public TestProductsInMemoryCreation()
        {
            
        }

        [Fact(DisplayName = "Adding a new product")]
        public void Add_New_Product_Check_By_ItsName()
        {
            ProductDataModel productdata = new ProductDataModel() { name = "TestProduct", colour = "Red" };
            var productrepo = new ProductMemoryRepo();
            productrepo.add(productdata);
            List<ProductDataModel> productsadded = productrepo.get();
            Assert.Equal(productdata.name, productsadded.FirstOrDefault().name);
        }

        [Theory(DisplayName ="Check if Green Colour Exists")]
        [InlineData("Product1", "Green")]
        public void Return_Specific_Colour_Products( string productname,string colour)
        {
            ProductDataModel productdata = new ProductDataModel() { name = productname, colour = colour };
            var productrepo = new ProductMemoryRepo();
            productrepo.add(productdata);
            List<ProductDataModel> productsadded = productrepo.get(colour);
            Assert.Equal(productdata.colour, productsadded.FirstOrDefault().colour);
        }
        [Theory(DisplayName ="Adding multiple products")]
        [InlineData("Product1","Green")]
        [InlineData("Product2", "Red")]
        [InlineData("Product3", "Blue")]

        public void Add_New_Product_Check_If_Its_Added(string productname, string colour)
        {
            ProductDataModel product = new ProductDataModel() { name = productname, colour = colour };
            var productrepo = new ProductMemoryRepo();
            productrepo.add(product);
            List<ProductDataModel> productsadded = productrepo.get();
            Assert.Equal(1, productsadded.Count);
        }

        [Fact]
        public void Return_Zero_Products()
        {
            var product = new ProductMemoryRepo();
            List<ProductDataModel> productsadded = product.get("Green");
            Assert.Equal(0, productsadded.Count());
        }
    }
}