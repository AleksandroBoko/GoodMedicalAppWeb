using System;
using System.Collections.Generic;
using System.Linq;
using GoodMedicalApp.Domain.Models;
using GoodMedicalApp.DataAccess.DataAccess.Implementation;
using GoodMedicalApp.BusinessServices.Mappers.Implementation;
using GoodMedicalApp.DataAccess.DataAccess;
using GoodMedicalApp.DataAccess;
using GoodMedicalApp.BusinessServices.Mappers;

namespace GoodMedicalApp.BusinessServices.Services.Implementation
{
    public class DoctorService : IDoctorService
    {
        public DoctorService()
        {
            doctorRepository = DoctorRepository.GetInstance();
            doctorMapper = new DoctorMapper();
        }

        private readonly IRepository<DoctorEntity> doctorRepository;
        private readonly IMapper<Doctor, DoctorEntity> doctorMapper;

        public void Add(Doctor item)
        {
            var doctorEntity = new DoctorEntity();
            doctorMapper.MapToEntity(item, doctorEntity);
            doctorRepository.Create(doctorEntity);            
        }

        public IList<Doctor> GetAll()
        {
            var entities = doctorRepository.GetList();
            var doctors = new List<Doctor>();

            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    var doctor = new Doctor();
                    doctorMapper.MapFromEntity(entity, doctor);
                    doctors.Add(doctor);
                }
            }

            return doctors;
        }

        public Doctor GetItemById(int id)
        {
            var entity = doctorRepository.GetItemById(id);
            if(entity != null)
            {
                var doctor = new Doctor();
                doctorMapper.MapFromEntity(entity, doctor);
                return doctor;
            }
            else
            {
                return null;
            }
        }

        public void Remove(Doctor item)
        {
            var entity = new DoctorEntity();
            doctorMapper.MapToEntity(item, entity);
            doctorRepository.Delete(entity);
        }

        public void Save()
        {
            doctorRepository.Save();
        }        
    }
}
