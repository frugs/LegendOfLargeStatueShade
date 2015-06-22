using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultDoorData {
        private readonly Id<IDoor> _id;
        private readonly Id<IDoor> _opposingDoorId;
        private readonly Vector2 _anchorLocationInRoomSpace;

        public DefaultDoorData(Id<IDoor> id, Id<IDoor> opposingDoorId, Vector2 anchorLocationInRoomSpace) {
            _id = id;
            _opposingDoorId = opposingDoorId;
            _anchorLocationInRoomSpace = anchorLocationInRoomSpace;
        }

        public Id<IDoor> Id1 {
            get { return _id; }
        }

        public Id<IDoor> OpposingDoorId {
            get { return _opposingDoorId; }
        }

        public Vector2 AnchorLocationInRoomSpace {
            get { return _anchorLocationInRoomSpace; }
        }
    }
}