using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataService.ConcreteClasses;
using InMemoryRepo.ConcreteClasses;
using InMemoryRepo.Interfaces;
using Moq;

namespace TestService
{
    public class TestProductServiceLayer
    {
        private readonly Mock<IProductInMemoryRepo> _mockingRepo = new Mock<IProductInMemoryRepo>();
        private readonly ProductService _sut;

        public TestProductServiceLayer()
        {
            _sut = new ProductService(_mockingRepo.Object);
        }

        [Theory]
        [InlineData("Product1","Green")]
        [InlineData("Product2", "Blue")]
        public void Add_New_Product_From_ServiceLayer(string productName,string colour)
        {
            ProductDataModel product = new ProductDataModel() { name = productName, colour = colour };
            _mockingRepo.Setup(x => x.add(product)).Returns(true);
            var result2 = _sut.add(product);
            Assert.True(result2);
        }

        [Theory]
        [InlineData("Product1", "Green")]
        [InlineData("Product2", "Blue")]
        public void Add_New_Product_From_ServiceLayer_Called_Only_Once(string productName, string colour)
        {
            ProductDataModel product = new ProductDataModel() { name= productName,colour= colour };
            _mockingRepo.Setup(x => x.add(product)).Returns(true);
            var result2 = _sut.add(product);
            _mockingRepo.Verify(serv=>serv.add(product),Times.Once);
        }

        [Theory]
        [InlineData("Product1", "Green")]
        public void Get_ProductOf_Sepcific_Color(string productName, string colour)
        {
            ProductDataModel product = new ProductDataModel() { name = productName, colour = colour };
            _mockingRepo.Setup(x => x.add(product)).Returns(true);
            var result2 = _sut.add(product);
            List<ProductDataModel> greenProduct = new List<ProductDataModel>() { new ProductDataModel() { name=productName, colour = colour } };
            _mockingRepo.Setup(x=>x.get(colour)).Returns(greenProduct);
            var result = _sut.get(colour);
            Assert.Equal(greenProduct, result);
        }

        [Theory]
        [InlineData("Product1", "Green")]
        public void Get_AllProucts_Color(string productName, string colour)
        {
            ProductDataModel product = new ProductDataModel() { name = productName, colour = colour };
            _mockingRepo.Setup(x => x.add(product)).Returns(true);
            var result2 = _sut.add(product);
            List<ProductDataModel> greenProduct = new List<ProductDataModel>() { new ProductDataModel() { name = productName, colour = colour } };
            _mockingRepo.Setup(x => x.get()).Returns(greenProduct);
            var result = _sut.getAll();
            Assert.Equal(greenProduct, result);
        }


    }
}
