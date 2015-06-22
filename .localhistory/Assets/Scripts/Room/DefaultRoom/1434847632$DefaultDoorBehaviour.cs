using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultDoorBehaviour : MonoBehaviour, IDoor {
        private Id<IDoor> _id;
        public Id<IDoor> Id { get { return _id; } }
        public Id<IDoor> OpposingDoorId { get; private set; }
        public Vector2 AnchorLocation { get; private set; }
    }
}