using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Scripts.Util;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomMap {
        private readonly IDictionary<Id<IRoom>, ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>>> _roomData =
            new Dictionary<Id<IRoom>, ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>>>();

        private readonly IDictionary<Id<IDoor>, Id<IRoom>> _doorIndex = new Dictionary<Id<IDoor>, Id<IRoom>>();

        public static readonly Id<IRoom> Room1Id = new Id<IRoom>(new Guid("9d7272d1-535a-44b4-a732-a943929f9a48"));
        public static readonly Id<IRoom> Room2Id = new Id<IRoom>(new Guid("a3aa9d93-2560-48ce-9cb3-306ff8b1b91e"));

        public DefaultRoomMap() {
            var room1Door1Id = new Id<IDoor>(new Guid());
            var room2Door1Id = new Id<IDoor>(new Guid());

            AddRoom(Room1Id, new List<KeyValuePair<Id<IDoor>, Id<IDoor>>> {
                new KeyValuePair<Id<IDoor>, Id<IDoor>>(room1Door1Id, room2Door1Id)
            });

            AddRoom(Room2Id, new List<KeyValuePair<Id<IDoor>, Id<IDoor>>> {
                new KeyValuePair<Id<IDoor>, Id<IDoor>>(room2Door1Id, room1Door1Id)
            });
        }

        private void AddRoom(Id<IRoom> roomId, List<KeyValuePair<Id<IDoor>, Id<IDoor>>> doors) {
            _roomData.Add(roomId, doors.AsReadOnly());

            foreach (var door in doors) {
                _doorIndex.Add(door.Key, roomId);
            }
        }

        public ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>> GetDoors(Id<IRoom> roomId) {
            return _roomData[roomId];
        }

        public Id<IRoom> GetRoomWithDoor(Id<IDoor> doorId) {
            return _doorIndex[doorId];
        }
    }
}