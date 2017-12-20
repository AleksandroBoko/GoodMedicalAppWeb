using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.DataAccess.DataAccess
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetList();
        T GetItemById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        void Save();
    }
}
