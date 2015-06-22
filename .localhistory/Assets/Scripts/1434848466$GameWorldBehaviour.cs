using Assets.Scripts.Room;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts {
    public class GameWorldBehaviour : MonoBehaviour {

        public RoomManager RoomManager { get; private set; }

        public void Start() {
            RoomManager = new RoomManager(new Coordinate(0, 0), new DefaultRoomFactory(), 100);
        }

        private class DefaultRoomFactory : IRoomFactory {
            public IRoom CreateRoom(Coordinate location) {
                return new DefaultRoom(location);
            }

            private class DefaultRoom : IRoom {
                private readonly Coordinate _location;

                public DefaultRoom(Coordinate location) {
                    _location = location;
                }

                public void Dispose() {
                    throw new System.NotImplementedException();
                }

                public Vector2 SizeInWorldUnits { get; private set; }
                public Vector2 CentreInWorldCoords { get; private set; }
            }
        }
    }
}
