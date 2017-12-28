using GoodMedicalApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.DataAccess.DataAccess.Implementation
{
    public class OperationRepository:IRepository<OperationEntity>
    {
        private OperationRepository()
        {
            medicalEntity = MedicalEntities.GetInstance();
        }

        private static OperationRepository instance;
        private static object rootSync = new object();

        private readonly MedicalEntities medicalEntity;

        public static IRepository<OperationEntity> GetInstance()
        {
            if (instance == null)
            {
                lock (rootSync)
                {
                    if (instance == null)
                    {
                        instance = new OperationRepository();
                    }
                }
            }

            return instance;
        }

        public void Create(OperationEntity item)
        {
            if (item == null)
            {
                return;
            }

            medicalEntity.OperationEntities.Add(item);
        }

        public void Delete(OperationEntity item)
        {
            if (item == null)
            {
                return;
            }

            var operation = medicalEntity.OperationEntities.FirstOrDefault(x => x.Id == item.Id);
            if(operation != null)
            {
                medicalEntity.OperationEntities.Remove(operation);
            }
            
        }

        public OperationEntity GetItemById(int id)
        {
            if (medicalEntity != null && medicalEntity.OperationEntities.Any())
            {
                return medicalEntity.OperationEntities.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<OperationEntity> GetList()
        {
            if (medicalEntity != null)
            {
                return medicalEntity.OperationEntities;
            }
            else
            {
                return null;
            }
        }

        public void Update(OperationEntity item)
        {
            if (item == null)
            {
                return;
            }

            if (medicalEntity != null)
            {
                var existingOperation = GetItemById(item.Id);
                existingOperation.TreatmentId = item.TreatmentId;
                existingOperation.TypeOperationId = item.TypeOperationId;
                existingOperation.Medicines = item.Medicines;
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
