using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomData {
        private readonly Id<IRoom> _id;
        private readonly Vector2 _size;
        private readonly Object _prefab;

        public DefaultRoomData(Id<IRoom> id, Vector2 size, Object prefab) {
            _prefab = prefab;
            _id = id;
            _size = size;
        }

        public Id<IRoom> Id {
            get { return _id; }
        }

        public Vector2 Size {
            get { return _size; }
        }

        public Object Prefab {
            get { return _prefab; }
        }
    }
}