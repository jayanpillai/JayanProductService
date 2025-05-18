using DataModel;
using DataService.Interfaces;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.ConcreteClasses
{
    public class UserService : IUserService
    {
        private IValidUserAccountRepo _userRepo;

        public UserService(IValidUserAccountRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public void add(UserDataModel user)
        {
            _userRepo.add(user);
        }

        public UserDataModel get(string username)
        {
            return _userRepo.get(username);
        }
    }
}
