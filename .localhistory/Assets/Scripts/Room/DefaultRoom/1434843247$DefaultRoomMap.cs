using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Scripts.Util;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomMap {

        private IDictionary<Id<IRoom>, ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>>> _roomData =
            new Dictionary<Id<IRoom>, ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>>>();

        public ReadOnlyCollection<KeyValuePair<Id<IDoor>, Id<IDoor>>> GetDoors(Id<IRoom> roomId) {
            return null;
        }
    }
}