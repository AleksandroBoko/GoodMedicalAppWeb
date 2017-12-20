using GoodMedicalApp.DataAccess;
using GoodMedicalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.BusinessServices.Mappers.Implementation
{
    class TypeOperationMapper : IMapper<TypeOperation, TypeOperationEntity>
    {
        public void MapFromEntity(TypeOperationEntity typeOperationEntity, TypeOperation typeOperation)
        {
            if (typeOperationEntity != null && typeOperation != null)
            {
                typeOperation.Id = typeOperationEntity.Id;
                typeOperation.Name = typeOperationEntity.Name;
            }
        }

        public void MapToEntity(TypeOperation typeOperation, TypeOperationEntity typeOperationEntity)
        {
            if (typeOperation != null && typeOperationEntity != null)
            {
                typeOperationEntity.Id = typeOperation.Id;
                typeOperationEntity.Name = typeOperation.Name;
            }
        }
    }
}
