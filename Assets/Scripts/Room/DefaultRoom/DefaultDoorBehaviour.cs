using System;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultDoorBehaviour : MonoBehaviour, IDoor {
        [SerializeField] private string _id;
        [SerializeField] private string _opposingId;
        [SerializeField] private GameObject _anchor;

        public Id<IDoor> Id {
            get { return new Id<IDoor>(new Guid(_id)); }
        }

        public Id<IDoor> OpposingDoorId {
            get { return new Id<IDoor>(new Guid(_opposingId)); }
        }

        public Vector2 AnchorLocation {
            get { return _anchor.transform.position; }
        }
    }
}