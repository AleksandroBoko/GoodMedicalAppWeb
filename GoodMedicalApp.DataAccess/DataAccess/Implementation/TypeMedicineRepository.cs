using GoodMedicalApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.DataAccess.DataAccess.Implementation
{
    public class TypeMedicineRepository:IRepository<TypeMedicineEntity>
    {
        private TypeMedicineRepository()
        {
            medicalEntity = MedicalEntities.GetInstance();
        }

        private static TypeMedicineRepository instance;
        private static object rootSync = new object();

        private readonly MedicalEntities medicalEntity;

        public static IRepository<TypeMedicineEntity> GetInstance()
        {
            if (instance == null)
            {
                lock (rootSync)
                {
                    if (instance == null)
                    {
                        instance = new TypeMedicineRepository();
                    }
                }
            }

            return instance;
        }

        public void Create(TypeMedicineEntity item)
        {
            if (item == null)
            {
                return;
            }

            medicalEntity.TypeMedicineEntities.Add(item);
        }

        public void Delete(TypeMedicineEntity item)
        {
            if (item == null)
            {
                return;
            }

            var typeMedicine = medicalEntity.TypeMedicineEntities.FirstOrDefault(x => x.Id == item.Id);
            if (typeMedicine != null)
            {
                if (typeMedicine.Medicines != null && typeMedicine.Medicines.Any())
                {
                    while (typeMedicine.Medicines.Count > 0)
                    {
                        var medicine = typeMedicine.Medicines.LastOrDefault();
                        medicalEntity.Entry(medicine).State = EntityState.Deleted;
                    }
                }

                medicalEntity.TypeMedicineEntities.Remove(typeMedicine);
            }
        }

        public TypeMedicineEntity GetItemById(int id)
        {
            if (medicalEntity != null && medicalEntity.TypeMedicineEntities.Any())
            {
                return medicalEntity.TypeMedicineEntities.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<TypeMedicineEntity> GetList()
        {
            if (medicalEntity != null)
            {
                return medicalEntity.TypeMedicineEntities;
            }
            else
            {
                return null;
            }
        }

        public void Update(TypeMedicineEntity item)
        {
            if (item == null)
            {
                return;
            }

            if (medicalEntity != null)
            {
                var existingTypeMedicine = GetItemById(item.Id);
                existingTypeMedicine.Name = item.Name;
            }
        }

        public void Save()
        {
            if (medicalEntity != null)
            {
                medicalEntity.SaveChanges();
            }
        }

    }
}
