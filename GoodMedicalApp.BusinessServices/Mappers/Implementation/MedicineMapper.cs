using GoodMedicalApp.DataAccess;
using GoodMedicalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.BusinessServices.Mappers.Implementation
{
    class MedicineMapper : IMapper<Medicine, MedicineEntity>
    {
        public MedicineMapper()
        {
            typeMedicineMapper = new TypeMedicineMapper();
        }

        private readonly IMapper<TypeMedicine, TypeMedicineEntity> typeMedicineMapper;

        public void MapToEntity(Medicine medicine, MedicineEntity medicineEntity)
        {
            if (medicine != null && medicineEntity != null)
            {
                medicineEntity.Id = medicine.Id;
                medicineEntity.Cost = medicine.Cost;
                medicineEntity.MethodUsing = medicine.MethodUsing;
                medicineEntity.Name = medicine.Name;

                if (medicine.CurrentTypeMedicine != null)
                {
                    var typeMedicineEntity = new TypeMedicineEntity();
                    typeMedicineMapper.MapToEntity(medicine.CurrentTypeMedicine, typeMedicineEntity);                    
                    medicineEntity.TypeMedicineId = typeMedicineEntity.Id;
                }
            }
        }

        public void MapFromEntity(MedicineEntity medicineEntity, Medicine medicine)
        {
            if (medicineEntity != null && medicine != null)
            {
                medicine.Id = medicineEntity.Id;
                medicine.Cost = medicineEntity.Cost;
                medicine.MethodUsing = medicineEntity.MethodUsing;
                medicine.Name = medicineEntity.Name;

                if (medicineEntity.TypeMedicine != null)
                {
                    var typeMedicine = new TypeMedicine();
                    typeMedicineMapper.MapFromEntity(medicineEntity.TypeMedicine, typeMedicine);
                    medicine.CurrentTypeMedicine = typeMedicine;
                }
            }
        }
    }
}
