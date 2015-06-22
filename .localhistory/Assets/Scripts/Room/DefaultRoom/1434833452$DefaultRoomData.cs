using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomData {
        private readonly Id<IRoom> _id;
        private readonly Object _prefab;
        private readonly IList<DefaultDoorData> _doors;

        public DefaultRoomData(Id<IRoom> id, Object prefab, IList<DefaultDoorData> doors) {
            _id = id;
            _prefab = prefab;
            _doors = doors;
        }

        public Id<IRoom> Id {
            get { return _id; }
        }

        public Object Prefab {
            get { return _prefab; }
        }

        public IList<DefaultDoorData> Doors {
            get { return _doors; }
        }
    }
}