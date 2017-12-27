using GoodMedicalApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.DataAccess.DataAccess.Implementation
{
    public class TreatmentRepository : IRepository<TreatmentEntity>
    {
        private TreatmentRepository()
        {
            medicalEntity = MedicalEntities.GetInstance();
        }

        private static TreatmentRepository instance;
        private static object rootSync = new object();

        private readonly MedicalEntities medicalEntity;

        public static IRepository<TreatmentEntity> GetInstance()
        {
            if (instance == null)
            {
                lock (rootSync)
                {
                    if (instance == null)
                    {
                        instance = new TreatmentRepository();
                    }
                }
            }

            return instance;
        }

        public void Create(TreatmentEntity item)
        {
            if (item == null)
            {
                return;
            }

            medicalEntity.TreatmentEntities.Add(item);
        }

        public void Delete(TreatmentEntity item)
        {
            if (item == null)
            {
                return;
            }

            var treatment = medicalEntity.TreatmentEntities.FirstOrDefault(x => x.Id == item.Id);
            if (treatment != null)
            {
                if (treatment.TreatmentReports != null && treatment.TreatmentReports.Any())
                {
                    while(treatment.TreatmentReports.Count > 0)
                    {
                        var treatmentReport = treatment.TreatmentReports.LastOrDefault();
                        medicalEntity.Entry(treatmentReport).State = EntityState.Deleted;
                    }
                }

                if (treatment.Operations != null && treatment.Operations.Any())
                {
                    while (treatment.Operations.Count > 0)
                    {
                        var operation = treatment.Operations.LastOrDefault();
                        medicalEntity.Entry(operation).State = EntityState.Deleted;
                    }
                }

                medicalEntity.TreatmentEntities.Remove(treatment);
            }
        }

        public TreatmentEntity GetItemById(int id)
        {
            if (medicalEntity != null && medicalEntity.TreatmentEntities.Any())
            {
                return medicalEntity.TreatmentEntities.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<TreatmentEntity> GetList()
        {
            if (medicalEntity != null)
            {
                return medicalEntity.TreatmentEntities;
            }
            else
            {
                return null;
            }
        }

        public void Update(TreatmentEntity item)
        {
            if (item == null)
            {
                return;
            }

            if (medicalEntity != null)
            {
                medicalEntity.TreatmentEntities.Remove(item);
                medicalEntity.TreatmentEntities.Add(item);
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
