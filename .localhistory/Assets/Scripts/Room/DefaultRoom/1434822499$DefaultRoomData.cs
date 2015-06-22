using System;
using Assets.Scripts.Util;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomData {
        private Id<IRoom> _id;
        private UnityEngine.Object _prefab;

        public Id<IRoom> Id {
            get { return _id; }
        }

        public UnityEngine.Object Prefab { get { return _prefab; } }
    }
}