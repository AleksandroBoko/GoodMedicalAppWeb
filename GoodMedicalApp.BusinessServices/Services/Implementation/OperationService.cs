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
    public class OperationService : IOperationService
    {
        public OperationService()
        {
            operationRepository = OperationRepository.GetInstance();
            operationMapper = new OperationMapper();
        }

        private readonly IRepository<OperationEntity> operationRepository;
        private readonly IMapper<Operation, OperationEntity> operationMapper;

        public void Add(Operation item)
        {
            var entity = new OperationEntity();
            operationMapper.MapToEntity(item, entity);
            operationRepository.Create(entity);
        }

        public IList<Operation> GetAll()
        {
            var entities = operationRepository.GetList();
            var operations = new List<Operation>();

            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    var operation = new Operation();
                    operationMapper.MapFromEntity(entity, operation);
                    operations.Add(operation);
                }
            }

            return operations;
        }

        public Operation GetItemById(int id)
        {
            var entity = operationRepository.GetItemById(id);
            if (entity != null)
            {
                var operation = new Operation();
                operationMapper.MapFromEntity(entity, operation);
                return operation;
            }
            else
            {
                return null;
            }
        }

        public void Remove(Operation item)
        {
            var entity = new OperationEntity();
            operationMapper.MapToEntity(item, entity);
            operationRepository.Delete(entity);
        }

        public void Save()
        {
            operationRepository.Save();
        }

        public void Update(Operation item)
        {
            throw new NotImplementedException();
        }
    }
}
