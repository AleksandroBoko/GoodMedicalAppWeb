using GoodMedicalApp.BusinessServices.Mappers;
using GoodMedicalApp.BusinessServices.Mappers.Implementation;
using GoodMedicalApp.DataAccess;
using GoodMedicalApp.DataAccess.DataAccess;
using GoodMedicalApp.DataAccess.DataAccess.Implementation;
using GoodMedicalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.BusinessServices.Services.Implementation
{
    public class TypeOperationService : ITypeOperationService
    {
        public TypeOperationService()
        {
            typeOperationRepository = TypeOperationRepository.GetInstance();
            typeOperationMapper = new TypeOperationMapper();
        }

        private readonly IRepository<TypeOperationEntity> typeOperationRepository;
        private readonly IMapper<TypeOperation, TypeOperationEntity> typeOperationMapper;

        public void Add(TypeOperation item)
        {
            var entity = new TypeOperationEntity();
            typeOperationMapper.MapToEntity(item, entity);
            typeOperationRepository.Create(entity);
        }

        public IList<TypeOperation> GetAll()
        {
            var typeOperationEntities = typeOperationRepository.GetList();
            var typeOperations = new List<TypeOperation>();

            if (typeOperationEntities.Any())
            {
                foreach (var typeOperationEntity in typeOperationEntities)
                {
                    var typeOperation = new TypeOperation();
                    typeOperationMapper.MapFromEntity(typeOperationEntity, typeOperation);
                    typeOperations.Add(typeOperation);
                }
            }

            return typeOperations;
        }

        public TypeOperation GetItemById(int id)
        {
            var entity = typeOperationRepository.GetItemById(id);
            if (entity != null)
            {
                var typeOperation = new TypeOperation();
                typeOperationMapper.MapFromEntity(entity, typeOperation);
                return typeOperation;
            }
            else
            {
                return null;
            }
        }

        public void Remove(TypeOperation item)
        {
            var typeOperationEntity = new TypeOperationEntity();
            typeOperationMapper.MapToEntity(item, typeOperationEntity);
            typeOperationRepository.Delete(typeOperationEntity);
        }

        public void Save()
        {
            typeOperationRepository.Save();
        }
    }
}
