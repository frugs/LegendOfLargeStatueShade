using System.Collections.ObjectModel;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomBehaviour : MonoBehaviour {
        
        [SerializeField]
        private Vector2 _roomSize;

        public void Awake() {
            
        }

        public Vector2 RoomSize {
            get { return _roomSize; }
        }

        public ReadOnlyCollection<IDoor> Doors { get; set; }
    }
}