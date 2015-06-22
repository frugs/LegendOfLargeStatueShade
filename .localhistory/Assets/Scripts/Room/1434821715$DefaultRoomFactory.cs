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

            private GameObject _roomObject;

            public DefaultRoom(Object prefab) {
                _roomObject = (GameObject) Object.Instantiate(prefab);
            }

            public void Dispose() {
                Object.Destroy(_roomObject);
            }

            public Vector2 SizeInWorldUnits { get; private set; }
            public Vector2 CentreInWorldCoords { get; private set; }
        }
    }
}