using GoodMedicalApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.DataAccess.DataAccess.Implementation
{
    public class TreatmentReportRepository : IRepository<TreatmentReportEntity>
    {
        private TreatmentReportRepository()
        {
            medicalEntity = MedicalEntities.GetInstance();
        }

        private static TreatmentReportRepository instance;
        private static object rootSync = new object();

        private readonly MedicalEntities medicalEntity;

        public static IRepository<TreatmentReportEntity> GetInstance()
        {
            if (instance == null)
            {
                lock (rootSync)
                {
                    if (instance == null)
                    {
                        instance = new TreatmentReportRepository();
                    }
                }
            }

            return instance;
        }

        public void Create(TreatmentReportEntity item)
        {
            if (item == null)
            {
                return;
            }

            medicalEntity.TreatmentReportEntities.Add(item);
        }

        public void Delete(TreatmentReportEntity item)
        {
            if (item == null)
            {
                return;
            }

            var treatmentReport = medicalEntity.TreatmentReportEntities.FirstOrDefault(x => x.Id == item.Id);
            if (treatmentReport != null)
            {
                medicalEntity.TreatmentReportEntities.Remove(treatmentReport);
            }
        }

        public TreatmentReportEntity GetItemById(int id)
        {
            if (medicalEntity != null && medicalEntity.TreatmentReportEntities != null
                    && medicalEntity.TreatmentReportEntities.Any())
            {
                return medicalEntity.TreatmentReportEntities.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<TreatmentReportEntity> GetList()
        {
            if (medicalEntity != null)
            {
                return medicalEntity.TreatmentReportEntities;
            }
            else
            {
                return null;
            }
        }

        public void Update(TreatmentReportEntity item)
        {
            if (item == null)
            {
                return;
            }

            if (medicalEntity != null)
            {
                var existingTreatmentEntity = GetItemById(item.Id);
                existingTreatmentEntity.Conclusion = item.Conclusion;
                existingTreatmentEntity.Comment = item.Comment;
                existingTreatmentEntity.TreatmentId = item.TreatmentId;
                existingTreatmentEntity.Medicines = item.Medicines;
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
