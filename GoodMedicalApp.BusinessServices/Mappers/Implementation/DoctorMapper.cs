using GoodMedicalApp.DataAccess;
using GoodMedicalApp.Domain.Models;

namespace GoodMedicalApp.BusinessServices.Mappers.Implementation
{
    public class DoctorMapper : IMapper<Doctor, DoctorEntity>
    {
        public void MapToEntity(Doctor doctor, DoctorEntity doctorEntity)
        {            
            if (doctor != null && doctorEntity != null)
            {
                doctorEntity.Id = doctor.Id;
                doctorEntity.FirstName = doctor.FirstName;
                doctorEntity.LastName = doctor.LastName;
                doctorEntity.Qualification = doctor.Qualification;
            }
        }

        public void MapFromEntity(DoctorEntity doctorEntity, Doctor doctor)
        {
            if (doctorEntity != null && doctor != null)
            {
                doctor.Id = doctorEntity.Id;
                doctor.FirstName = doctorEntity.FirstName;
                doctor.LastName = doctorEntity.LastName;
                doctor.Qualification = doctorEntity.Qualification;
            }
        }
    }
}
