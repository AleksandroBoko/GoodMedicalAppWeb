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
    public class MedicineService : IMedicineService
    {
        public MedicineService()
        {
            medicineRepository = MedicineRepository.GetInstance();
            medicineMapper = new MedicineMapper();
        }

        private readonly IRepository<MedicineEntity> medicineRepository;
        private readonly IMapper<Medicine, MedicineEntity> medicineMapper;

        public void Add(Medicine item)
        {
            var entity = new MedicineEntity();
            medicineMapper.MapToEntity(item, entity);
            medicineRepository.Create(entity);
        }

        public IList<Medicine> GetAll()
        {
            var entities = medicineRepository.GetList();
            var medicines = new List<Medicine>();

            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    var medicine = new Medicine();
                    medicineMapper.MapFromEntity(entity, medicine);
                    medicines.Add(medicine);
                }
            }

            return medicines;
        }

        public Medicine GetItemById(int id)
        {
            var entity = medicineRepository.GetItemById(id);
            if (entity != null)
            {
                var medicine = new Medicine();
                medicineMapper.MapFromEntity(entity, medicine);
                return medicine;
            }
            else
            {
                return null;
            }
        }

        public void Remove(Medicine item)
        {
            var entity = new MedicineEntity();
            medicineMapper.MapToEntity(item, entity);
            medicineRepository.Delete(entity);
        }

        public void Save()
        {
            medicineRepository.Save();
        }

        public void Update(Medicine item)
        {
            throw new NotImplementedException();
        }
    }
}
