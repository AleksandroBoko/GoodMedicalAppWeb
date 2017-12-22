using GoodMedicalApp.BusinessServices.Services.Implementation;
using GoodMedicalApp.DataAccess;
using GoodMedicalApp.DataAccess.DataAccess.Implementation;
using GoodMedicalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.BusinessServices.Mappers.Implementation
{
    class OperationMapper : IMapper<Operation, OperationEntity>
    {
        public OperationMapper()
        {
            typeOperationMapper = new TypeOperationMapper();
            medicineMapper = new MedicineMapper();
        }

        private readonly IMapper<TypeOperation,TypeOperationEntity> typeOperationMapper;
        private readonly IMapper<Medicine, MedicineEntity> medicineMapper;

        public void MapToEntity(Operation operation, OperationEntity operationEntity)
        {
            if (operation != null && operationEntity != null)
            {
                operationEntity.Id = operation.Id;
                operationEntity.TreatmentId = operation.TreatmentId;

                if (operation.CurrentTypeOperation != null)
                {
                    var typeOperationEntity = new TypeOperationEntity();
                    typeOperationMapper.MapToEntity(operation.CurrentTypeOperation, typeOperationEntity);
                    operationEntity.TypeOperationId = typeOperationEntity.Id;
                }

                if (operation.Medicines.Any())
                {
                    var medcicineRepository = MedicineRepository.GetInstance();

                    foreach (var medicine in operation.Medicines)
                    {
                        var medicineEntity = medcicineRepository.GetItemById(medicine.Id);                        
                        operationEntity.Medicines.Add(medicineEntity);
                    }                    
                }
            }
        }

        public void MapFromEntity(OperationEntity operationEntity, Operation operation)
        {
            if (operationEntity != null && operation != null)
            {
                operation.Id = operationEntity.Id;
                operation.TreatmentId = operationEntity.TreatmentId;

                if (operationEntity.TypeOperation != null)
                {
                    var typeOperation = new TypeOperation();
                    typeOperationMapper.MapFromEntity(operationEntity.TypeOperation, typeOperation);
                    operation.CurrentTypeOperation = typeOperation;
                }
                
                if(operationEntity.Medicines.Any())
                {
                    var medicines = new List<Medicine>();
                    foreach(var medicineEntity in operationEntity.Medicines)
                    {
                        var medicine = new Medicine();
                        medicineMapper.MapFromEntity(medicineEntity, medicine);
                        medicines.Add(medicine);
                    }

                    operation.Medicines = medicines;
                }
            }
        }
    }
}
