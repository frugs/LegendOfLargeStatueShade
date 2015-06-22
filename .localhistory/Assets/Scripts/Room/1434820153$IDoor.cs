using System;
using UnityEditor;

namespace Assets.Scripts.Room {
    public interface IDoor {
        DoorID ID { get; }
        DoorID OpposingDoorID { get; }
    }

    public class DoorID {
        private readonly GUID _guid;

        public DoorID(GUID guid) {
            _guid = guid;
        }

        public GUID ToGuid() {
            return _guid;
        }

        public String ToString() {
            return _guid.ToString();
        }
    }
}