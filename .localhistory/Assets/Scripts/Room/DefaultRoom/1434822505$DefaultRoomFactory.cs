using System;
using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomFactory : IRoomFactory {

        private readonly IDictionary<Id<IRoom>, RoomData> 

        public IRoom CreateRoom(Id<IRoom> roomId) {
            throw new NotImplementedException();
        }

        public IRoom CreateAdjacentRoom(IDoor door) {
            throw new NotImplementedException();
        }

        public class DefaultRoom : IRoom {
            private readonly Id<IRoom> _id;
            private readonly GameObject _roomObject;
            private readonly Vector2 _size;

            public DefaultRoom(Id<IRoom> id, Vector2 size, Object prefab) {
                _id = id;
                _size = size;
                _roomObject = (GameObject) Object.Instantiate(prefab);
            }

            public void Dispose() {
                Object.Destroy(_roomObject);
            }

            public Id<IRoom> Id {
                get { return _id; }
            }

            public Vector2 Size {
                get { return _size; }
            }

            public Vector2 Centre {
                get { return _roomObject.transform.position; }
            }
        }
    }
}