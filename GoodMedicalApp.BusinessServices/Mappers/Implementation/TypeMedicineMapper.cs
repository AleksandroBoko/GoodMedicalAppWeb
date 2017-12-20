using GoodMedicalApp.DataAccess;
using GoodMedicalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.BusinessServices.Mappers.Implementation
{

    class TypeMedicineMapper : IMapper<TypeMedicine, TypeMedicineEntity>
    {
        public void MapFromEntity(TypeMedicineEntity typeMedicineEntity, TypeMedicine typeMedicine)
        {
            if (typeMedicineEntity != null && typeMedicine != null)
            {
                typeMedicine.Id = typeMedicineEntity.Id;
                typeMedicine.Name = typeMedicineEntity.Name;
            }
        }

        public void MapToEntity(TypeMedicine typeMedicine, TypeMedicineEntity typeMedicineEntity)
        {
            if (typeMedicine != null && typeMedicineEntity != null)
            {
                typeMedicineEntity.Id = typeMedicine.Id;
                typeMedicineEntity.Name = typeMedicine.Name;
            }
        }
    }

}
