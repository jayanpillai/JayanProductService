using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IValidUserAccountRepo
    {
        void add(UserDataModel user);

        UserDataModel get(string username);
    }
}
