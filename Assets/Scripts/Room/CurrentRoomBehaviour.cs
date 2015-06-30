using System;
using Assets.Scripts.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts.Room {
    internal class CurrentRoomBehaviour : MonoBehaviour {
        private readonly CurrentRoomModelImpl _currentRoomModel = new CurrentRoomModelImpl();

        [SerializeField] private RoomBehaviour _startingRoom;

        public void Awake() {
            _currentRoomModel.CurrentRoom = _startingRoom;
            _currentRoomModel.CurrentRoomUpdated = (fromRoom, toRoom) => { };
        }

        public void Start() {
            _currentRoomModel.CurrentRoom.Activate();
            RegisterRoomCallbacks(_currentRoomModel.CurrentRoom);
        }

        public void OnDestroy() {
            UnregisterRoomCallbacks(_currentRoomModel.CurrentRoom);
        }

        private void UpdateCurrentRoom(PlayerBehaviour player, DoorBehaviour door) {
            var fromRoom = _currentRoomModel.CurrentRoom;
            var toRoom = door.OpposingDoor.Room;

            _currentRoomModel.CurrentRoom = toRoom;
            _currentRoomModel.CurrentRoomUpdated(fromRoom, toRoom);

            UnregisterRoomCallbacks(fromRoom);
            RegisterRoomCallbacks(toRoom);
        }

        private void RegisterRoomCallbacks(RoomBehaviour room) {
            foreach (var door in room.Doors) {
                door.PlayerExitedThroughDoor += UpdateCurrentRoom;
            }
        }

        private void UnregisterRoomCallbacks(RoomBehaviour room) {
            foreach (var door in room.Doors) {
                door.PlayerExitedThroughDoor -= UpdateCurrentRoom;
            }
        }

        public ICurrentRoomModel CurrentRoomModel {
            get { return _currentRoomModel; }
        }

        private class CurrentRoomModelImpl : ICurrentRoomModel {
            public RoomBehaviour CurrentRoom { get; set; }
            public Action<RoomBehaviour, RoomBehaviour> CurrentRoomUpdated { get; set; }
        }
    }
}