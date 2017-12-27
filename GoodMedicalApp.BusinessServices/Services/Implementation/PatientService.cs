using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodMedicalApp.Domain.Models;
using GoodMedicalApp.DataAccess.DataAccess.Implementation;
using GoodMedicalApp.DataAccess;
using GoodMedicalApp.DataAccess.DataAccess;
using GoodMedicalApp.BusinessServices.Mappers.Implementation;
using GoodMedicalApp.BusinessServices.Mappers;

namespace GoodMedicalApp.BusinessServices.Services.Implementation
{
    public class PatientService : IPatientService
    {
        public PatientService()
        {
            patientRepository = PatientRepository.GetInstance();
            patientMapper = new PatientMapper();
        }

        private readonly IRepository<PatientEntity> patientRepository;
        private readonly IMapper<Patient, PatientEntity> patientMapper;

        public void Add(Patient item)
        {
            var entity = new PatientEntity();
            patientMapper.MapToEntity(item, entity);
            patientRepository.Create(entity);
        }

        public IList<Patient> GetAll()
        {
            var entities = patientRepository.GetList();
            var patients = new List<Patient>();

            if(entities.Any())
            {
                foreach(var entity in entities)
                {
                    var patient = new Patient();
                    patientMapper.MapFromEntity(entity, patient);
                    patients.Add(patient);
                }
            }

            return patients;
        }

        public Patient GetItemById(int id)
        {
            var entity = patientRepository.GetItemById(id);
            if (entity != null)
            {
                var patient = new Patient();
                patientMapper.MapFromEntity(entity, patient);
                return patient;
            }
            else
            {
                return null;
            }
        }

        public void Remove(Patient item)
        {
            var entity = new PatientEntity();
            patientMapper.MapToEntity(item, entity);
            patientRepository.Delete(entity);
        }

        public void Save()
        {
            patientRepository.Save();
        }

        public void Update(Patient item)
        {
            throw new NotImplementedException();
        }
    }
}
