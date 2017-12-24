using GoodMedicalApp.DataAccess;
using GoodMedicalApp.DataAccess.DataAccess.Implementation;
using GoodMedicalApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace GoodMedicalApp.BusinessServices.Mappers.Implementation
{
    class TreatmentReportMapper : IMapper<TreatmentReport, TreatmentReportEntity>
    {
        public TreatmentReportMapper()
        {
            treatmentMapper = new TreatmentMapper();
            medicineMapper = new MedicineMapper();
        }

        private readonly IMapper<Treatment,TreatmentEntity> treatmentMapper;
        private readonly IMapper<Medicine, MedicineEntity> medicineMapper;

        public void MapFromEntity(TreatmentReportEntity treatmentReportEntity, TreatmentReport treatmentReport)
        {
            if (treatmentReportEntity != null && treatmentReport != null)
            {
                treatmentReport.Id = treatmentReportEntity.Id;
                treatmentReport.Conclusion = treatmentReportEntity.Conclusion;
                treatmentReport.Comment = treatmentReportEntity.Comment;

                if (treatmentReportEntity.Treatment != null)
                {
                    var treatment = new Treatment();
                    treatmentMapper.MapFromEntity(treatmentReportEntity.Treatment, treatment);
                    treatmentReport.CurrentTreatment = treatment;
                }

                if (treatmentReportEntity.Medicines.Any())
                {
                    var medicines = new List<Medicine>();
                    foreach (var medicineEntity in treatmentReportEntity.Medicines)
                    {
                        var medicine = new Medicine();
                        medicineMapper.MapFromEntity(medicineEntity, medicine);
                        medicines.Add(medicine);
                    }

                    treatmentReport.Medicines = medicines;
                }
            }
        }

        public void MapToEntity(TreatmentReport treatmentReport, TreatmentReportEntity treatmentReportEntity)
        {
            if (treatmentReportEntity != null && treatmentReport != null)
            {
                treatmentReportEntity.Id = treatmentReport.Id;
                treatmentReportEntity.Conclusion = treatmentReport.Conclusion;
                treatmentReportEntity.Comment = treatmentReport.Comment;

                if (treatmentReport.CurrentTreatment != null)
                {
                    var treatmentEntity = new TreatmentEntity();
                    treatmentMapper.MapToEntity(treatmentReport.CurrentTreatment, treatmentEntity);
                    treatmentReportEntity.TreatmentId = treatmentEntity.Id;
                }

                if (treatmentReport.Medicines.Any())
                {
                    var medicineRepository = MedicineRepository.GetInstance();
                
                    foreach (var medicine in treatmentReport.Medicines)
                    {
                        var medicineEntity = medicineRepository.GetItemById(medicine.Id);
                        treatmentReportEntity.Medicines.Add(medicineEntity);
                    }
                }
            }
        }
    }
}
