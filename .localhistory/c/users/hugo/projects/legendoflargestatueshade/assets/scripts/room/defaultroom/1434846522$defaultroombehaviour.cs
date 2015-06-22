using System.Collections.ObjectModel;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomBehaviour : MonoBehaviour, IRoom {
        
        [SerializeField]
        private Vector2 _roomSize;

        public void Awake() {
            
        }

        public Vector2 RoomSize {
            get { return _roomSize; }
        }

        public ReadOnlyCollection<IDoor> Doors { get; set; }
        public Id<IRoom> Id { get; set; }
        public Vector2 Size { get; private set; }
        public Vector2 Centre { get; private set; }

        public void SetDoorIds(int doorIndex, Id<IDoor> doorId, Id<IDoor> oppositeId) {
            throw new System.NotImplementedException();
        }

        public void Dispose() {
            Destroy(gameObject);
        }
    }
}