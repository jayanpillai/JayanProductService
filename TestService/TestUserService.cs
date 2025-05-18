using DataModel;
using DataService.ConcreteClasses;
using InMemoryRepo.Interfaces;
using Moq;
using Repository.Interfaces;


namespace TestService
{
    public class TestUserService
    {
        private readonly Mock<IValidUserAccountRepo> _mockingRepo = new Mock<IValidUserAccountRepo>();
        private readonly UserService _sut;

        public TestUserService()
        {
            _sut = new UserService(_mockingRepo.Object);
        }

        [Theory]
        [InlineData("UserName1")]
        public void Add_New_User(string userName)
        {
            UserDataModel user = new UserDataModel { UserName = userName };
            _mockingRepo.Setup(x => x.add(user));
            _sut.add(user);
            _mockingRepo.Setup(x => x.get(userName)).Returns(user);
            var UserAdded = _sut.get(userName);
            Assert.Equal(user, UserAdded);
        }

        [Theory]
        [InlineData("UserName2")]
        public void User_Does_Not_Exist(string userName)
        {
            UserDataModel user = null;
            _mockingRepo.Setup(x => x.get(userName)).Returns(user);
            var UserAdded = _sut.get(userName);
            Assert.Equal(user, UserAdded);
        }
    }
}
