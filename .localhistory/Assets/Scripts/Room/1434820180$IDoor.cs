using System;
using UnityEditor;

namespace Assets.Scripts.Room {
    public interface IDoor {
        DoorID Id { get; }
        DoorID OpposingDoorId { get; }
    }

    public class DoorID {
        private readonly GUID _guid;

        public DoorID(GUID guid) {
            _guid = guid;
        }

        public GUID ToGuid() {
            return _guid;
        }

        public override String ToString() {
            return _guid.ToString();
        }
    }
}