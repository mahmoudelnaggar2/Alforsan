using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public interface IParentService
    {
        List<Parent> GetAll();
        Parent GetParent(int id);
        void CreateParent(Parent parent);
        void UpdateParent(Parent parent);
        void DeleteParent(Parent parent);
        void SaveParent();
    }
}
