using GoodMedicalApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.DataAccess.DataAccess.Implementation
{
    public class PatientRepository:IRepository<PatientEntity>
    {
        private PatientRepository()
        {
            medicalEntity = MedicalEntities.GetInstance();
        }

        private static PatientRepository instance;
        private static object rootSync = new object();

        private readonly MedicalEntities medicalEntity;

        public static IRepository<PatientEntity> GetInstance()
        {
            if (instance == null)
            {
                lock (rootSync)
                {
                    if (instance == null)
                    {
                        instance = new PatientRepository();
                    }
                }
            }

            return instance;
        }

        public void Create(PatientEntity item)
        {
            if (item == null)
            {
                return;
            }

            medicalEntity.PatientEntities.Add(item);
        }

        public void Delete(PatientEntity item)
        {
            if (item == null)
            {
                return;
            }

            var treatmentRepository = TreatmentRepository.GetInstance();
            var patient = medicalEntity.PatientEntities.FirstOrDefault(x => x.Id == item.Id);
            if(patient != null)
            {
                if(patient.Treatments != null && patient.Treatments.Any())
                {
                    while(patient.Treatments.Count > 0)
                    {
                        var treatment = patient.Treatments.LastOrDefault();
                        treatmentRepository.Delete(treatment);
                    }
                }

                medicalEntity.PatientEntities.Remove(patient);
            }
            
        }

        public PatientEntity GetItemById(int id)
        {
            if (medicalEntity != null && medicalEntity.PatientEntities.Any())
            {
                return medicalEntity.PatientEntities.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<PatientEntity> GetList()
        {
            if (medicalEntity != null)
            {
                return medicalEntity.PatientEntities;
            }
            else
            {
                return null;
            }
        }

        public void Update(PatientEntity item)
        {
            if (item == null)
            {
                return;
            }

            if (medicalEntity != null)
            {
                var existingPatient = GetItemById(item.Id);
                existingPatient.FirstName = item.FirstName;
                existingPatient.LastName = item.LastName;
                existingPatient.Age = item.Age;
                existingPatient.RoomId = item.RoomId;
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
