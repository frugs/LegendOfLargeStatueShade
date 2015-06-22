using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Scripts.Util;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomBehaviour : MonoBehaviour, IRoom {
        [SerializeField] private string _id;

        [SerializeField] private Vector2 _roomSize;

        public Vector2 RoomSize {
            get { return _roomSize; }
        }

        public ReadOnlyCollection<IDoor> Doors {
            get { return new List<IDoor>(gameObject.GetComponents<DefaultDoorBehaviour>()).AsReadOnly(); }
        }

        public Id<IRoom> Id {
            get { return new Id<IRoom>(new GUID(_id)); }
        }

        public Vector2 Size {
            get { return _roomSize; }
        }

        public Vector2 Centre {
            get { return gameObject.transform.position; }
        }

        public void Dispose() {
            Destroy(gameObject);
        }
    }
}