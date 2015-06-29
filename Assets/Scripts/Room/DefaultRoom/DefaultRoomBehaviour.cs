using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomBehaviour : RoomBehaviour {
        [SerializeField] private Vector2 _roomSize;

        public override Rect Area {
            get {
                Vector2 position = transform.position;
                var topLeft = position - (_roomSize / 2);
                return new Rect(topLeft.x, topLeft.y, _roomSize.x, _roomSize.y);
            }
        }

        public override ReadOnlyCollection<DoorBehaviour> Doors {
            get {
                var doors = gameObject.GetComponentsInChildren<DoorBehaviour>(true);
                return new List<DoorBehaviour>(doors).AsReadOnly();
            }
        }

        public override void Activate() {
            gameObject.SetActive(true);
        }

        public override void Deactivate() {
            gameObject.SetActive(false);
        }

        public void Awake() {
            Deactivate();
        }
    }
}