using System;
using Assets.Scripts.Util;
using UnityEditor;

namespace Assets.Scripts.Room {
    public interface IDoor {
        Id<IDoor> Id { get; }
        Id<IDoor> OpposingDoorId { get; }
    }
}