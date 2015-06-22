using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Scripts.Util;
using UnityEditor;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomMap {
        private IDictionary<Id<IRoom>, ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>>> _roomData =
            new Dictionary<Id<IRoom>, ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>>>();

        public DefaultRoomMap() {
            var room1Door1Id = new Id<IDoor>(new GUID());
            var room2Door1Id = new Id<IDoor>(new GUID());

            _roomData.Add(new Id<IRoom>(new GUID()), new ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>> {
                new KeyValuePair<Id<IDoor>, Id<IDoor>>(room1Door1Id, room2Door1Id)
            });
        }

        public ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>> GetDoors(Id<IRoom> roomId) {
            return null;
        }
    }
}