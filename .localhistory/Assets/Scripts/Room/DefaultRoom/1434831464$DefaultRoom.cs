using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoom : IRoom {
        private readonly Id<IRoom> _id;
        private readonly GameObject _roomObject;
        private readonly List<IDoor> _doors = new List<IDoor>();

        public DefaultRoom(Id<IRoom> id, Object prefab, ICollection<DefaultDoorData> doors) {
            _id = id;
            _roomObject = (GameObject) Object.Instantiate(prefab);

            foreach (var doorData in doors) {
                _doors.Add(new DefaultDoor(doorData.Id, doorData.OpposingDoorId, doorData.AnchorLocationInRoomSpace, this));
            }
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
            get { return _size; }
        }

        public ReadOnlyCollection<IDoor> Doors {
            get { return _doors.AsReadOnly(); }
        }

        public GameObject GameObject {
            get { return _roomObject; }
        }
    }
}