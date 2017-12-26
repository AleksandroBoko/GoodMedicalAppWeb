using GoodMedicalApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.DataAccess.DataAccess.Implementation
{
    public class RoomRepository : IRepository<RoomEntity>
    {
        private RoomRepository()
        {
            medicalEntity = MedicalEntities.GetInstance();
        }

        private static RoomRepository instance;
        private static object rootSync = new object();

        private readonly MedicalEntities medicalEntity;

        public static IRepository<RoomEntity> GetInstance()
        {
            if (instance == null)
            {
                lock (rootSync)
                {
                    if (instance == null)
                    {
                        instance = new RoomRepository();
                    }
                }
            }

            return instance;
        }

        public void Create(RoomEntity item)
        {
            if (item == null)
            {
                return;
            }

            medicalEntity.RoomEntities.Add(item);
        }

        public void Delete(RoomEntity item)
        {
            if (item == null)
            {
                return;
            }

            var room = medicalEntity.RoomEntities.FirstOrDefault(x => x.Id == item.Id);
            if (room != null)
            {
                medicalEntity.RoomEntities.Remove(room);
            }
        }

        public RoomEntity GetItemById(int id)
        {
            if (medicalEntity != null && medicalEntity.RoomEntities.Any())
            {
                return medicalEntity.RoomEntities.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<RoomEntity> GetList()
        {
            if (medicalEntity != null)
            {
                return medicalEntity.RoomEntities;
            }
            else
            {
                return null;
            }
        }

        public void Update(RoomEntity item)
        {
            if (item == null)
            {
                return;
            }

            if (medicalEntity != null)
            {
                medicalEntity.RoomEntities.Remove(item);
                medicalEntity.RoomEntities.Add(item);
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
