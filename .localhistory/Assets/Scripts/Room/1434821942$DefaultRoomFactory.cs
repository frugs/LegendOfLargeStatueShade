using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room {
    public class DefaultRoomFactory : IRoomFactory {
        public IRoom CreateRoom(Id<IRoom> roomId) {
            throw new System.NotImplementedException();
        }

        public IRoom CreateAdjacentRoom(IDoor door) {
            throw new System.NotImplementedException();
        }

        private class DefaultRoom : IRoom {

            private readonly GameObject _roomObject;
            private readonly Id<IRoom> _id;
            private readonly Vector2 _size;

            public DefaultRoom(Object prefab, Id<IRoom> id, Vector2 size) {
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
            public Vector2 Size { get { return _size; } }
            public Vector2 Centre { get; private set; }
        }
    }
}