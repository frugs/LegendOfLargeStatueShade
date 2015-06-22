using Assets.Scripts.Room.DefaultRoom;
using UnityEngine;

namespace Assets.Scripts.Room {
    public class RoomManagerBehaviour : MonoBehaviour {

        private IRoomFactory _rooomFactory;

        public IRoom CurrentRoom { get; set; }

        void Awake() {
            _rooomFactory = GetComponentInChildren<DefaultRoomFactoryBehaviour>();
        }

        void Start() {
            CurrentRoom = _rooomFactory.CreateRoom(DefaultRoomMap.Room1Id);
        }
    }
}