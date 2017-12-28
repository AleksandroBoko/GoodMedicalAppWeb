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
            medicineService = new MedicineService();
        }

        private readonly IRepository<OperationEntity> operationRepository;
        private readonly IMapper<Operation, OperationEntity> operationMapper;
        private readonly IMedicineService medicineService;

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

        public OperationTransfer GetTransferItemById(int id)
        {
            var operation = GetItemById(id);
            if (operation != null)
            {
                var currentMedicines = new List<int>();
                if (operation.Medicines != null && operation.Medicines.Any())
                {
                    currentMedicines = operation.Medicines.Select(x => x.Id).ToList();
                }

                var operationTransfer = new OperationTransfer()
                {
                    Id = operation.Id,
                    CurrentTypeOperation = operation.CurrentTypeOperation,
                    TreatmentId = operation.TreatmentId,
                    Medicines = currentMedicines
                };

                return operationTransfer;
            }
            else
            {
                throw new ArgumentException("The operation object is null");
            }
        }

        public Operation GetItemFromTransferItem(OperationTransfer operationTransfer)
        {
            if (operationTransfer != null)
            {
                var operation = new Operation() { Id = operationTransfer.Id };
                operation.CurrentTypeOperation = new TypeOperation() { Id = operationTransfer.CurrentTypeOperation.Id };
                operation.TreatmentId = operationTransfer.TreatmentId;
                if (operationTransfer.Medicines.Any())
                {
                    operation.Medicines = new List<Medicine>();
                    foreach (var id in operationTransfer.Medicines)
                    {
                        var medicine = medicineService.GetItemById(id);
                        operation.Medicines.Add(medicine);
                    }
                }

                return operation;
            }
            else
            {
                throw new ArgumentException("The tranfer object is null");
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
            var operationEntity = new OperationEntity();
            operationMapper.MapToEntity(item, operationEntity);
            operationRepository.Update(operationEntity);
        }
    }
}
