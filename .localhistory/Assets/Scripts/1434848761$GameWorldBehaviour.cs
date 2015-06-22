using Assets.Scripts.Room;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts {
    public class GameWorldBehaviour : MonoBehaviour {

        public RoomManager RoomManager { get; private set; }

        public void Start() {
            RoomManager = new RoomManager(new Coordinate(0, 0), new DefaultRoomFactory(), 100);
        }
    }
}
