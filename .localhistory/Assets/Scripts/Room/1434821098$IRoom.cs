using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Room {
    public interface IRoom : IDisposable {
        RoomId Id { get; }
        Vector2 SizeInWorldUnits { get; }
        Vector2 CentreInWorldCoords { get; }
    }

    public class RoomId {
        private readonly GUID _guid;

        public RoomId(GUID guid) {
            _guid = guid;
        }

        public GUID ToGuid() {
            return _guid;
        }

        public override string ToString() {
            return _guid.ToString();
        }
    }
}