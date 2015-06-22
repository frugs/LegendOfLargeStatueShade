using Assets.Scripts.Room.DefaultRoom;
using UnityEngine;

namespace Assets.Scripts.Room {
    public class RoomManagerBehaviour : MonoBehaviour {
        private IRoomFactory _rooomFactory;

        public IRoom CurrentRoom { get; private set; }

        public void Awake() {
            _rooomFactory = GetComponent<DefaultRoomFactoryBehaviour>();
        }

        public void Start() {
            CurrentRoom = _rooomFactory.CreateRoom(DefaultRoomMap.Room1Id);
        }
    }
}