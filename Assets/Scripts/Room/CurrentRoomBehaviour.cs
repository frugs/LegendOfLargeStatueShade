using System;
using Assets.Scripts.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts.Room {
    internal class CurrentRoomBehaviour : MonoBehaviour {
        private readonly CurrentRoomModelImpl _currentRoomModel = new CurrentRoomModelImpl();

        [SerializeField] private RoomBehaviour _startingRoom;

        public ICurrentRoomModel CurrentRoomModel {
            get { return _currentRoomModel; }
        }

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

        private void RegisterRoomCallbacks(RoomBehaviour room) {
            foreach (var door in room.Doors) {
                door.PlayerExitedThroughDoor += UpdateCurrentRoomThroughDoor;
            }

            foreach (var areaTransition in room.AreaTransitions) {
                areaTransition.OldAreaFadeOutFinished += UpdateCurrentRoomThroughAreaTransition;
            }
        }

        private void UnregisterRoomCallbacks(RoomBehaviour room) {
            foreach (var door in room.Doors) {
                door.PlayerExitedThroughDoor -= UpdateCurrentRoomThroughDoor;
            }

            foreach (var areaTransition in room.AreaTransitions) {
                areaTransition.OldAreaFadeOutFinished -= UpdateCurrentRoomThroughAreaTransition;
            }
        }

        private void UpdateCurrentRoom(RoomBehaviour fromRoom, RoomBehaviour toRoom) {
            _currentRoomModel.CurrentRoom = toRoom;
            _currentRoomModel.CurrentRoomUpdated(fromRoom, toRoom);

            UnregisterRoomCallbacks(fromRoom);
            RegisterRoomCallbacks(toRoom);
        }

        private void UpdateCurrentRoomThroughDoor(PlayerBehaviour player, DoorBehaviour door) {
            var fromRoom = _currentRoomModel.CurrentRoom;
            var toRoom = door.OpposingDoor.Room;

            UpdateCurrentRoom(fromRoom, toRoom);
        }

        private void UpdateCurrentRoomThroughAreaTransition(AreaTransitionBehaviour areaTransition) {
            var fromRoom = _currentRoomModel.CurrentRoom;
            var toRoom = areaTransition.OpposingAreaTransition.Room;

            UpdateCurrentRoom(fromRoom, toRoom);
        }

        private class CurrentRoomModelImpl : ICurrentRoomModel {
            public RoomBehaviour CurrentRoom { get; set; }
            public Action<RoomBehaviour, RoomBehaviour> CurrentRoomUpdated { get; set; }
        }
    }
}