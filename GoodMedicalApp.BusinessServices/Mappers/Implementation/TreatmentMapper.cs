using GoodMedicalApp.DataAccess;
using GoodMedicalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.BusinessServices.Mappers.Implementation
{
    class TreatmentMapper : IMapper<Treatment, TreatmentEntity>
    {
        public void MapFromEntity(TreatmentEntity item, Treatment targetItem)
        {
            if (item != null && targetItem != null)
            {
                targetItem.Id = item.Id;
                targetItem.DoctorId = item.DoctorId;
                targetItem.PatientId = item.PatientId;
                targetItem.StartDate = item.StartDate;
            }
        }

        public void MapToEntity(Treatment item, TreatmentEntity targetItem)
        {
            if (item != null && targetItem != null)
            {
                targetItem.Id = item.Id;
                targetItem.DoctorId = item.DoctorId;
                targetItem.PatientId = item.PatientId;
                targetItem.StartDate = item.StartDate;
            }
        }
    }
}
