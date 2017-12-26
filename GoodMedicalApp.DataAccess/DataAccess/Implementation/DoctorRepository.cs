using GoodMedicalApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.DataAccess.DataAccess.Implementation
{
    public class DoctorRepository : IRepository<DoctorEntity>
    {
        private DoctorRepository()
        {
            medicalEntity = MedicalEntities.GetInstance();
        }

        private static DoctorRepository instance;
        private static object rootSync = new object();

        private readonly MedicalEntities medicalEntity;

        public static IRepository<DoctorEntity> GetInstance()
        {
            if(instance == null)
            {
                lock(rootSync)
                {
                    if(instance == null)
                    {
                        instance = new DoctorRepository();
                    }
                }
            }

            return instance; 
        }

        public void Create(DoctorEntity item)
        {
            if (item == null)
            {
                return;
            }

            medicalEntity.DoctorEntities.Add(item);
        }

        public void Delete(DoctorEntity item)
        {
            if (item == null)
            {
                return;
            }

            var treatmentRepository = TreatmentRepository.GetInstance();
            var doctor = medicalEntity.DoctorEntities.FirstOrDefault(x => x.Id == item.Id);
            if (doctor != null)
            {
                if (doctor.Treatments != null && doctor.Treatments.Any())
                {
                    while (doctor.Treatments.Count > 0)
                    {
                        var treatment = doctor.Treatments.LastOrDefault();
                        treatmentRepository.Delete(treatment);
                    }
                }

                medicalEntity.DoctorEntities.Remove(doctor);
            }
        }

        public DoctorEntity GetItemById(int id)
        {
            if (medicalEntity != null && medicalEntity.DoctorEntities.Any())
            {
                return medicalEntity.DoctorEntities.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<DoctorEntity> GetList()
        {
            if(medicalEntity != null)
            {
                return medicalEntity.DoctorEntities;
            }
            else
            {
                return null;
            }
        }

        public void Update(DoctorEntity item)
        {
            if(item == null)
            {
                return;
            }

            if(medicalEntity != null)
            {
                medicalEntity.DoctorEntities.Remove(item);
                medicalEntity.DoctorEntities.Add(item);
            }
        }

        public void Save()
        {
            if(medicalEntity != null)
            {
                medicalEntity.SaveChanges();
            }
        }
    }
}
