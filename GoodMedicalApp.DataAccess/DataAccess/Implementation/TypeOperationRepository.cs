using GoodMedicalApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.DataAccess.DataAccess.Implementation
{
    public class TypeOperationRepository:IRepository<TypeOperationEntity>
    {
        private TypeOperationRepository()
        {
            medicalEntity = MedicalEntities.GetInstance();
        }

        private static TypeOperationRepository instance;
        private static object rootSync = new object();

        private readonly MedicalEntities medicalEntity;

        public static IRepository<TypeOperationEntity> GetInstance()
        {
            if (instance == null)
            {
                lock (rootSync)
                {
                    if (instance == null)
                    {
                        instance = new TypeOperationRepository();
                    }
                }
            }

            return instance;
        }

        public void Create(TypeOperationEntity item)
        {
            if (item == null)
            {
                return;
            }

            medicalEntity.TypeOperationEntities.Add(item);
        }

        public void Delete(TypeOperationEntity item)
        {
            if (item == null)
            {
                return;
            }

            medicalEntity.TypeOperationEntities.Remove(item);
        }

        public TypeOperationEntity GetItemById(int id)
        {
            if (medicalEntity != null && medicalEntity.TypeOperationEntities.Any())
            {
                return medicalEntity.TypeOperationEntities.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<TypeOperationEntity> GetList()
        {
            if (medicalEntity != null)
            {
                return medicalEntity.TypeOperationEntities;
            }
            else
            {
                return null;
            }
        }

        public void Update(TypeOperationEntity item)
        {
            if (item == null)
            {
                return;
            }

            if (medicalEntity != null)
            {
                medicalEntity.TypeOperationEntities.Remove(item);
                medicalEntity.TypeOperationEntities.Add(item);
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
