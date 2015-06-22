using System;
using UnityEditor;

namespace Assets.Scripts.Room {
    public interface IDoor {
        DoorId Id { get; }
        DoorId OpposingDoorId { get; }
    }

    public class DoorId {
        private readonly GUID _guid;

        public DoorId(GUID guid) {
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