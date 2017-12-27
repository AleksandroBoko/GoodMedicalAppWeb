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
    public class TreatmentService : ITreatmentService
    {
        public TreatmentService()
        {
            treatmentRepository = TreatmentRepository.GetInstance();
            treatmentMapper = new TreatmentMapper();
        }

        private readonly IRepository<TreatmentEntity> treatmentRepository;
        private readonly IMapper<Treatment, TreatmentEntity> treatmentMapper;

        public void Add(Treatment treatment)
        {
            var treatmentEntity = new TreatmentEntity();
            treatmentMapper.MapToEntity(treatment, treatmentEntity);
            treatmentRepository.Create(treatmentEntity);
        }

        public IList<Treatment> GetAll()
        {
            var treatmentEntities = treatmentRepository.GetList();
            var treatments = new List<Treatment>();

            if (treatmentEntities.Any())
            {
                foreach (var treatmentEntity in treatmentEntities)
                {
                    var treatment = new Treatment();
                    treatmentMapper.MapFromEntity(treatmentEntity, treatment);
                    treatments.Add(treatment);
                }
            }

            return treatments;
        }

        public Treatment GetItemById(int id)
        {
            var treatmentEntity = treatmentRepository.GetItemById(id);
            if (treatmentEntity != null)
            {
                var treatment = new Treatment();
                treatmentMapper.MapFromEntity(treatmentEntity, treatment);
                return treatment;
            }
            else
            {
                return null;
            }
        }

        public void Remove(Treatment treatment)
        {
            var treatmentEntity = new TreatmentEntity();
            treatmentMapper.MapToEntity(treatment, treatmentEntity);
            treatmentRepository.Delete(treatmentEntity);
        }

        public void Save()
        {
            treatmentRepository.Save();
        }

        public void Update(Treatment item)
        {
            throw new NotImplementedException();
        }
    }
}
