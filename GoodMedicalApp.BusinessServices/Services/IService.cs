using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.BusinessServices.Services
{
    public interface IService<T>
    {
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        IList<T> GetAll();
        T GetItemById(int id);
        void Save();
    }
}
