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
    public class TypeMedicineService : ITypeMedicineService
    {
        public TypeMedicineService()
        {
            typeMedicineRepository = TypeMedicineRepository.GetInstance();
            typeMedicineMapper = new TypeMedicineMapper();
        }

        private readonly IRepository<TypeMedicineEntity> typeMedicineRepository;
        private readonly IMapper<TypeMedicine, TypeMedicineEntity> typeMedicineMapper;

        public void Add(TypeMedicine item)
        {
            var entity = new TypeMedicineEntity();
            typeMedicineMapper.MapToEntity(item, entity);
            typeMedicineRepository.Create(entity);
        }

        public IList<TypeMedicine> GetAll()
        {
            var typeMedicineEntities = typeMedicineRepository.GetList();
            var TypeMedicines = new List<TypeMedicine>();

            if (typeMedicineEntities.Any())
            {
                foreach (var typeMedicineEntity in typeMedicineEntities)
                {
                    var typeMedicine = new TypeMedicine();
                    typeMedicineMapper.MapFromEntity(typeMedicineEntity, typeMedicine);
                    TypeMedicines.Add(typeMedicine);
                }
            }

            return TypeMedicines;
        }

        public TypeMedicine GetItemById(int id)
        {
            var entity = typeMedicineRepository.GetItemById(id);
            if (entity != null)
            {
                var typeMedicine = new TypeMedicine();
                typeMedicineMapper.MapFromEntity(entity, typeMedicine);
                return typeMedicine;
            }
            else
            {
                return null;
            }
        }

        public void Remove(TypeMedicine item)
        {
            var typeMedicineEntity = new TypeMedicineEntity();
            typeMedicineMapper.MapToEntity(item, typeMedicineEntity);
            typeMedicineRepository.Delete(typeMedicineEntity);
        }

        public void Save()
        {
            typeMedicineRepository.Save();
        }

        public void Update(TypeMedicine item)
        {
            var typeMedicineEntity = new TypeMedicineEntity();
            typeMedicineMapper.MapToEntity(item, typeMedicineEntity);
            typeMedicineRepository.Update(typeMedicineEntity);
        }
    }
}
