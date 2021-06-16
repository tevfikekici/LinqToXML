using LinxToXml.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinxToXml.Interfaces
{
    public interface IUser
    {
        void Add(User user);
        void Update(User user);
        void Delete(int ID);

        DataSet GetAll();

    }
}
