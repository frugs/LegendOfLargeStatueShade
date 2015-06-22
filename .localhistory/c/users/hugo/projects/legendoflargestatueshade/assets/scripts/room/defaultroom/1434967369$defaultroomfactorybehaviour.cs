using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomFactoryBehaviour : MonoBehaviour, IRoomFactory {
        private readonly IDictionary<Id<IRoom>, GameObject> _rooms = new Dictionary<Id<IRoom>, GameObject>();
        private readonly IDictionary<Id<IDoor>, Id<IRoom>> _doorIndex = new Dictionary<Id<IDoor>, Id<IRoom>>();

        [SerializeField] private GameObject _roomsRootObject;

        public void Awake() {
            for (int i = 0; i < _roomsRootObject.transform.childCount; i++) {
                AddRoom(_roomsRootObject.transform.GetChild(i).gameObject);
            }
        }

        private void AddRoom(GameObject roomObject) {
            var roomId = roomObject.GetComponent<IRoom>().Id;
            _rooms.Add(roomId, roomObject);
            foreach (var door in roomObject.GetComponentsInChildren<IDoor>()) {
                _doorIndex.Add(door.Id, roomId);
            }
        }

        public IRoom CreateRoom(Id<IRoom> roomId) {
            return _rooms[roomId].GetComponent<DefaultRoomBehaviour>();
        }

        public IRoom CreateAdjacentRoom(IDoor door) {
            var roomId = _doorIndex[door.OpposingDoorId];
            var adjacentRoom = _rooms[roomId].GetComponent<DefaultRoomBehaviour>();
            AlignRoom(adjacentRoom, door);
            return adjacentRoom;
        }

        private void AlignRoom(DefaultRoomBehaviour adjacentRoom, IDoor door) {
            var adjacentDoor = adjacentRoom.Doors.Single(roomDoor => door.OpposingDoorId.Equals(roomDoor.Id));
            var delta = adjacentDoor.AnchorLocation - door.AnchorLocation;
            adjacentRoom.gameObject.transform.Translate(delta);
        }
    }
}