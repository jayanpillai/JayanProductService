using DataModel;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ConcreteClasses
{
    public class ValidUserAccountRepo : IValidUserAccountRepo
    {
        private List<UserDataModel> _validUsers;
        public ValidUserAccountRepo()
        {
            _validUsers = new List<UserDataModel>(); 
        }

        public void add(UserDataModel user)
        {
            _validUsers.Add(user);
        }

        public UserDataModel get(string username)
        {
            return _validUsers.Where(us => us.UserName == username).FirstOrDefault();
        }
    }
}
