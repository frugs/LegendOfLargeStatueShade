using Assets.Scripts.Room.DefaultRoom;
using UnityEngine;

namespace Assets.Scripts.Room {
    public class RoomManagerBehaviour : MonoBehaviour {

        private IRoomFactory _rooomFactory;

        void Awake() {
            _rooomFactory = GetComponentInChildren<DefaultRoomFactoryBehaviour>();
        }
    }
}