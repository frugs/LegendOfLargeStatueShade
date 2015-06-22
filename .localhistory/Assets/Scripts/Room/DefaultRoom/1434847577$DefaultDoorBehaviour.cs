using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultDoorBehaviour : MonoBehaviour, IDoor {
        public Id<IDoor> Id { get; private set; }
        public Id<IDoor> OpposingDoorId { get; private set; }
        public Vector2 AnchorLocation { get; private set; }
    }
}