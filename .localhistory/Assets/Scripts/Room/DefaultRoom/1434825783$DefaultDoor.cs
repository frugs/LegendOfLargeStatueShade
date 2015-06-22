using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    internal class DefaultDoor : IDoor {
        private readonly Id<IDoor> _id;
        private readonly Id<IDoor> _opposingDoorId;
        private readonly Vector2 _anchorLocationInRoomSpace;
        private readonly DefaultRoom _room;

        public DefaultDoor(Id<IDoor> id, Id<IDoor> opposingDoorId, Vector2 anchorLocationInRoomSpace, DefaultRoom room) {
            _id = id;
            _opposingDoorId = opposingDoorId;
            _anchorLocationInRoomSpace = anchorLocationInRoomSpace;
            _room = room;
        }

        public Id<IDoor> Id {
            get { return _id; }
        }

        public Id<IDoor> OpposingDoorId {
            get { return _opposingDoorId; }
        }

        public Vector2 AnchorLocation {
            get {
                Vector2 roomPosition = _room.GameObject.transform.position;
                return _anchorLocationInRoomSpace + roomPosition;
            }
        }
    }
}