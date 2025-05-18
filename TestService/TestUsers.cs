using DataModel;
using Repository.ConcreteClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestService
{
    public class TestUsers
    {
        [Fact]
        public void Add_New_User_Test()
        {
            UserDataModel User = new UserDataModel() { UserName = "ServiceAccountTest" };
            ValidUserAccountRepo validUserAccountRepo = new ValidUserAccountRepo();
            validUserAccountRepo.add(User);
            var Result = validUserAccountRepo.get("ServiceAccountTest");
            Assert.Equal(User, Result);
        }

        [Fact]
        public void User_Does_Not_Exist()
        {
            ValidUserAccountRepo validUserAccountRepo = new ValidUserAccountRepo();
            var Result = validUserAccountRepo.get("ServiceAccountTest");
            Assert.Equal(null, Result);
        }
    }
}
