using Assets.Scripts.Util;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultDoorBehaviour : MonoBehaviour, IDoor {
        
        [SerializeField] private GUID _id;
        [SerializeField] private GUID _opposingId;
        [SerializeField] private GameObject _anchor;

        public Id<IDoor> Id {
            get { return new Id<IDoor>(_id); }
        }

        public Id<IDoor> OpposingDoorId {
            get { return new Id<IDoor>(_opposingId); }
        }

        public Vector2 AnchorLocation { get; private set; }
    }
}