﻿using GoodMedicalApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.DataAccess.DataAccess.Implementation
{
    public class TypeOperationRepository : IRepository<TypeOperationEntity>
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

            var typeOperation = medicalEntity.TypeOperationEntities.FirstOrDefault(x => x.Id == item.Id);
            if (typeOperation != null)
            {
                if (typeOperation.Operations != null && typeOperation.Operations.Any())
                {
                    while (typeOperation.Operations.Count > 0)
                    {
                        var operation = typeOperation.Operations.LastOrDefault();
                        medicalEntity.Entry(operation).State = EntityState.Deleted;
                    }
                }

                medicalEntity.TypeOperationEntities.Remove(typeOperation);
            }
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
                var existingTypeOperation = GetItemById(item.Id);
                existingTypeOperation.Name = item.Name;
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
