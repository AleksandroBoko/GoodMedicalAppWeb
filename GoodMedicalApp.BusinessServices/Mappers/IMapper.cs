using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.BusinessServices.Mappers
{
    interface IMapper<T,K> where T : class
                           where K : class 
    {
        void MapToEntity(T item, K targetItem);
        void MapFromEntity(K item, T targetItem);
    }
}
