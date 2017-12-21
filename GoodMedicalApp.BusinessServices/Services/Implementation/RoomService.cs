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
    public class RoomService : IRoomService
    {
        public RoomService()
        {
            roomRepository = RoomRepository.GetInstance();
            roomMapper = new RoomMapper();
        }

        private readonly IRepository<RoomEntity> roomRepository;
        private readonly IMapper<Room, RoomEntity> roomMapper;

        public void Add(Room item)
        {
            var entity = new RoomEntity();
            roomMapper.MapToEntity(item, entity);
            roomRepository.Create(entity);
        }

        public IList<Room> GetAll()
        {
            var entities = roomRepository.GetList();
            var rooms = new List<Room>();

            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    var room = new Room();
                    roomMapper.MapFromEntity(entity, room);
                    rooms.Add(room);
                }
            }

            return rooms;
        }

        public Room GetItemById(int id)
        {
            var entity = roomRepository.GetItemById(id);
            if (entity != null)
            {
                var room = new Room();
                roomMapper.MapFromEntity(entity, room);
                return room;
            }
            else
            {
                return null;
            }
        }

        public void Remove(Room item)
        {
            var entity = new RoomEntity();
            roomMapper.MapToEntity(item, entity);
            roomRepository.Delete(entity);
        }

        public void Save()
        {
            roomRepository.Save();
        }
    }
}
