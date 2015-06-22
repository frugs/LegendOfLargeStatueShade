using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Util;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomFactoryBehaviour : MonoBehaviour, IRoomFactory {

        private static readonly Id<IRoom> Room1Id = new Id<IRoom>(new GUID("9d7272d1-535a-44b4-a732-a943929f9a48"));
        private static readonly Id<IRoom> Room2Id = new Id<IRoom>(new GUID("a3aa9d93-2560-48ce-9cb3-306ff8b1b91e"));

        private readonly IDictionary<Id<IRoom>, GameObject> _rooms = new Dictionary<Id<IRoom>, GameObject>();

        private readonly DefaultRoomMap _roomMap = new DefaultRoomMap();

        [SerializeField] private GameObject _room1Prefab;

        [SerializeField] private GameObject _room2Prefab;

        public void Awake() {
            _rooms.Add(Room1Id, _room1Prefab);
            _rooms.Add(Room2Id, _room2Prefab);
        }

        public IRoom CreateRoom(Id<IRoom> roomId) {
            var room = CreateDefaultRoom(roomId);
            room.GameObject.SetActive(true);
            return room;
        }

        public IRoom CreateAdjacentRoom(IDoor door) {
            var adjacentRoom = CreateDefaultRoom(_roomMap.GetRoomWithDoor(door.OpposingDoorId));
            AlignRoom(adjacentRoom, door);
            return adjacentRoom;
        }

        private DefaultRoom CreateDefaultRoom(Id<IRoom> roomId) {
            var room = Instantiate(_rooms[roomId]);
            room.GetComponent<DefaultRoomBehaviour>().Id = roomId

            return new DefaultRoom(roomId, _room1Prefab);
        }

        private void AlignRoom(DefaultRoom adjacentRoom, IDoor door) {
            var adjacentDoor = adjacentRoom.Doors.Single(roomDoor => door.OpposingDoorId.Equals(roomDoor.Id));
            var delta = adjacentDoor.AnchorLocation - door.AnchorLocation;
            adjacentRoom.GameObject.transform.Translate(delta);
        }
    }
}