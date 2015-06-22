using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Scripts.Util;
using UnityEditor;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomMap {
        private IDictionary<Id<IRoom>, ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>>> _roomData =
            new Dictionary<Id<IRoom>, ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>>>();

        private static readonly Id<IDoor> Room1Id = new Id<IDoor>(new GUID("9d7272d1-535a-44b4-a732-a943929f9a48"));
        private static readonly Id<IDoor> Room2Id = new Id<IDoor>(new GUID("a3aa9d93-2560-48ce-9cb3-306ff8b1b91e"));

        public DefaultRoomMap() {
            var room1Door1Id = new Id<IDoor>(new GUID());
            var room2Door1Id = new Id<IDoor>(new GUID());

            room1Id = ;
            _roomData.Add(new Id<IRoom>(room1Id), new List<KeyValuePair<Id<IDoor>, Id<IDoor>>> {
                new KeyValuePair<Id<IDoor>, Id<IDoor>>(room1Door1Id, room2Door1Id)
            }.AsReadOnly());

            _roomData.Add(new Id<IRoom>(new GUID()), new List<KeyValuePair<Id<IDoor>, Id<IDoor>>> {
                new KeyValuePair<Id<IDoor>, Id<IDoor>>(room2Door1Id, room1Door1Id)
            }.AsReadOnly());
        }

        public ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>> GetDoors(Id<IRoom> roomId) {
            return null;
        }
    }
}