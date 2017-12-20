using GoodMedicalApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.DataAccess.DataAccess.Implementation
{
    public class MedicineRepository:IRepository<MedicineEntity>
    {
        private MedicineRepository()
        {
            medicalEntity = MedicalEntities.GetInstance();
        }

        private static MedicineRepository instance;
        private static object rootSync = new object();

        private readonly MedicalEntities medicalEntity;

        public static IRepository<MedicineEntity> GetInstance()
        {
            if (instance == null)
            {
                lock (rootSync)
                {
                    if (instance == null)
                    {
                        instance = new MedicineRepository();
                    }
                }
            }

            return instance;
        }

        public void Create(MedicineEntity item)
        {
            if (item == null)
            {
                return;
            }

            medicalEntity.MedicineEntities.Add(item);
        }

        public void Delete(MedicineEntity item)
        {
            if (item == null)
            {
                return;
            }

            medicalEntity.MedicineEntities.Remove(item);
        }

        public MedicineEntity GetItemById(int id)
        {
            if (medicalEntity != null && medicalEntity.MedicineEntities.Any())
            {
                return medicalEntity.MedicineEntities.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<MedicineEntity> GetList()
        {
            if (medicalEntity != null)
            {
                return medicalEntity.MedicineEntities;
            }
            else
            {
                return null;
            }
        }

        public void Update(MedicineEntity item)
        {
            if (item == null)
            {
                return;
            }

            if (medicalEntity != null)
            {
                medicalEntity.MedicineEntities.Remove(item);
                medicalEntity.MedicineEntities.Add(item);
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
