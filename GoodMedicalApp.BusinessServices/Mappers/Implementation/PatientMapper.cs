using GoodMedicalApp.DataAccess;
using GoodMedicalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.BusinessServices.Mappers.Implementation
{
    class PatientMapper : IMapper<Patient, PatientEntity>
    {
        public PatientMapper()
        {
            roomMapper = new RoomMapper();
        }

        private readonly IMapper<Room, RoomEntity> roomMapper;

        public void MapToEntity(Patient patient, PatientEntity patientEntity)
        {
            if (patient != null && patientEntity != null)
            {
                patientEntity.Id = patient.Id;
                patientEntity.FirstName = patient.FirstName;
                patientEntity.LastName = patient.LastName;
                patientEntity.Age = patient.Age;

                if (patient.HospitalRoom != null)
                {
                    var roomEntity = new RoomEntity();
                    roomMapper.MapToEntity(patient.HospitalRoom, roomEntity);
                    patientEntity.Room = roomEntity;
                }
            }
        }

        public void MapFromEntity(PatientEntity patientEntity, Patient patient)
        {
            if (patientEntity != null && patient != null)
            {
                patient.Id = patientEntity.Id;
                patient.FirstName = patientEntity.FirstName;
                patient.LastName = patientEntity.LastName;
                patient.Age = patientEntity.Age;

                if (patientEntity.Room != null)
                {
                    var room = new Room();
                    roomMapper.MapFromEntity(patientEntity.Room, room);
                    patient.HospitalRoom = room;
                }
            }
        }
    }
}
