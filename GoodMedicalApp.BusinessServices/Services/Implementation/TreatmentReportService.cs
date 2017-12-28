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
    public class TreatmentReportService : ITreatmentReportService
    {
        public TreatmentReportService()
        {
            treatmentReportRepository = TreatmentReportRepository.GetInstance();
            treatmentReportMapper = new TreatmentReportMapper();
            medicineService = new MedicineService();
        }

        private readonly IRepository<TreatmentReportEntity> treatmentReportRepository;
        private readonly IMapper<TreatmentReport, TreatmentReportEntity> treatmentReportMapper;
        private readonly IMedicineService medicineService;

        public void Add(TreatmentReport item)
        {
            var entity = new TreatmentReportEntity();
            treatmentReportMapper.MapToEntity(item, entity);
            treatmentReportRepository.Create(entity);
        }

        public IList<TreatmentReport> GetAll()
        {
            var entities = treatmentReportRepository.GetList();
            var treatmentReports = new List<TreatmentReport>();

            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    var treatmentReport = new TreatmentReport();
                    treatmentReportMapper.MapFromEntity(entity, treatmentReport);
                    treatmentReports.Add(treatmentReport);
                }
            }

            return treatmentReports;
        }

        public TreatmentReport GetItemById(int id)
        {
            var entity = treatmentReportRepository.GetItemById(id);
            if (entity != null)
            {
                var treatmentReport = new TreatmentReport();
                treatmentReportMapper.MapFromEntity(entity, treatmentReport);
                return treatmentReport;
            }
            else
            {
                return null;
            }
        }

        public void Remove(TreatmentReport item)
        {
            var entity = new TreatmentReportEntity();
            treatmentReportMapper.MapToEntity(item, entity);
            treatmentReportRepository.Delete(entity);
        }

        public void Save()
        {
            treatmentReportRepository.Save();
        }

        public void Update(TreatmentReport item)
        {
            var treatmentReportEntity = new TreatmentReportEntity();
            treatmentReportMapper.MapToEntity(item, treatmentReportEntity);
            treatmentReportRepository.Update(treatmentReportEntity);
        }

        public TreatmentReportTransfer GetTransferItemById(int id)
        {
            var report = GetItemById(id);
            if (report != null)
            {
                var currentMedicines = new List<int>();
                if (report.Medicines != null && report.Medicines.Any())
                {
                    currentMedicines = report.Medicines.Select(x => x.Id).ToList();
                }

                var reportTransfer = new TreatmentReportTransfer()
                {
                    Id = report.Id,
                    CurrentTreatment = report.CurrentTreatment,
                    Conclusion = report.Conclusion,
                    Comment = report.Comment,
                    Medicines = currentMedicines
                };

                return reportTransfer;
            }
            else
            {
                throw new ArgumentException("The treatment report object is null");
            }
        }

        public TreatmentReport GetItemFromTransferItem(TreatmentReportTransfer treatmentReportTransfer)
        {
            if (treatmentReportTransfer != null)
            {
                var treatmentReport = new TreatmentReport() { Id = treatmentReportTransfer.Id };
                treatmentReport.CurrentTreatment = treatmentReportTransfer.CurrentTreatment;
                treatmentReport.Conclusion = treatmentReportTransfer.Conclusion;
                treatmentReport.Comment = treatmentReportTransfer.Comment;

                if (treatmentReportTransfer.Medicines.Any())
                {
                    treatmentReport.Medicines = new List<Medicine>();
                    foreach (var id in treatmentReportTransfer.Medicines)
                    {
                        var medicine = medicineService.GetItemById(id);
                        treatmentReport.Medicines.Add(medicine);
                    }
                }

                return treatmentReport;
            }
            else
            {
                throw new ArgumentException("The tranfer treatment report object is null");
            }
        }
    }
}
