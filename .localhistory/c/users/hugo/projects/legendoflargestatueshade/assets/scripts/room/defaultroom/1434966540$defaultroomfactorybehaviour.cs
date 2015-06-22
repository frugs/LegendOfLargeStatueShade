using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomFactoryBehaviour : MonoBehaviour, IRoomFactory {
        private readonly IDictionary<Id<IRoom>, GameObject> _rooms = new Dictionary<Id<IRoom>, GameObject>();

        private readonly DefaultRoomMap _roomMap = new DefaultRoomMap();

        [SerializeField] private GameObject _room1Prefab;

        [SerializeField] private GameObject _room2Prefab;

        public void Awake() {
            _rooms.Add(DefaultRoomMap.Room1Id, _room1Prefab);
            _rooms.Add(DefaultRoomMap.Room2Id, _room2Prefab);
        }

        public IRoom CreateRoom(Id<IRoom> roomId) {
            var room = CreateDefaultRoom(roomId);
            room.gameObject.SetActive(true);
            return room;
        }

        public IRoom CreateAdjacentRoom(IDoor door) {
            var adjacentRoom = CreateDefaultRoom(_roomMap.GetRoomWithDoor(door.OpposingDoorId));
            AlignRoom(adjacentRoom, door);
            return adjacentRoom;
        }

        private DefaultRoomBehaviour CreateDefaultRoom(Id<IRoom> roomId) {
            var room = Instantiate(_rooms[roomId]);
            return room.GetComponent<DefaultRoomBehaviour>();
        }

        private void AlignRoom(DefaultRoomBehaviour adjacentRoom, IDoor door) {
            var adjacentDoor = adjacentRoom.Doors.Single(roomDoor => door.OpposingDoorId.Equals(roomDoor.Id));
            var delta = adjacentDoor.AnchorLocation - door.AnchorLocation;
            adjacentRoom.gameObject.transform.Translate(delta);
        }
    }
}