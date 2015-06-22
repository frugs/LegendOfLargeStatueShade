using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomFactoryBehaviour : MonoBehaviour, IRoomFactory {
        private readonly IDictionary<Id<IRoom>, GameObject> _rooms = new Dictionary<Id<IRoom>, GameObject>();

        private readonly DefaultRoomMap _roomMap = new DefaultRoomMap();

        [SerializeField] private GameObject _roomsRootObject;

        public void Awake() {
            for (int i = 0; i < _roomsRootObject.transform.childCount; i++) {
                var roomObject = _roomsRootObject.transform.GetChild(i).gameObject;
                _rooms.Add(roomObject.GetComponent<IRoom>().Id, roomObject);
            }
        }

        public IRoom CreateRoom(Id<IRoom> roomId) {
            return CreateDefaultRoom(roomId);
        }

        public IRoom CreateAdjacentRoom(IDoor door) {
            var adjacentRoom = CreateDefaultRoom(_roomMap.GetRoomWithDoor(door.OpposingDoorId));
            AlignRoom(adjacentRoom, door);
            return adjacentRoom;
        }

        private DefaultRoomBehaviour CreateDefaultRoom(Id<IRoom> roomId) {
            return _rooms[roomId].GetComponent<DefaultRoomBehaviour>();
        }

        private void AlignRoom(DefaultRoomBehaviour adjacentRoom, IDoor door) {
            var adjacentDoor = adjacentRoom.Doors.Single(roomDoor => door.OpposingDoorId.Equals(roomDoor.Id));
            var delta = adjacentDoor.AnchorLocation - door.AnchorLocation;
            adjacentRoom.gameObject.transform.Translate(delta);
        }
    }
}