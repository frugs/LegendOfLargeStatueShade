using Assets.Scripts.Util;
using System.Collections.Generic;

namespace Assets.Scripts.Room.DefaultRoom {
    public class RoomMap {
        private readonly IDictionary<Id<RoomBehaviour>, RoomBehaviour> _rooms =
            new Dictionary<Id<RoomBehaviour>, RoomBehaviour>();

        private readonly IDictionary<Id<DoorBehaviour>, Id<RoomBehaviour>> _doorIndex =
            new Dictionary<Id<DoorBehaviour>, Id<RoomBehaviour>>();

        public void AddRoom(RoomBehaviour room) {
            _rooms.Add(room.Id, room);
            foreach (var door in room.Doors) {
                _doorIndex.Add(door.Id, room.Id);
            }
        }

        public RoomBehaviour GetRoom(Id<RoomBehaviour> roomId) {
            return _rooms[roomId];
        }

        public RoomBehaviour GetAdjacentRoom(DoorBehaviour door) {
            var roomId = _doorIndex[door.OpposingDoorId];
            return _rooms[roomId];
        }
    }
}