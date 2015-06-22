using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoom : IRoom {
        private readonly Id<IRoom> _id;
        private readonly GameObject _roomObject;

        public DefaultRoom(Id<IRoom> id, Object prefab) {
            _id = id;
            _roomObject = (GameObject) Object.Instantiate(prefab);
        }

        public void Dispose() {
            Object.Destroy(_roomObject);
        }

        public Id<IRoom> Id {
            get { return _id; }
        }

        public Vector2 Centre {
            get { return _roomObject.transform.position; }
        }

        public Vector2 Size {
            get { return _roomObject.GetComponent<DefaultRoomBehaviour>().RoomSize; }
        }

        public ReadOnlyCollection<IDoor> Doors {
            get { return _doors.AsReadOnly(); }
        }

        public GameObject GameObject {
            get { return _roomObject; }
        }
    }
}