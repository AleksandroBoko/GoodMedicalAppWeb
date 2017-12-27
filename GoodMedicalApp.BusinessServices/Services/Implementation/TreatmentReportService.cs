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
        }

        private readonly IRepository<TreatmentReportEntity> treatmentReportRepository;
        private readonly IMapper<TreatmentReport, TreatmentReportEntity> treatmentReportMapper;

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
            throw new NotImplementedException();
        }
    }
}
