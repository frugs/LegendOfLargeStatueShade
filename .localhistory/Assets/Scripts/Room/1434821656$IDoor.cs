using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room {
    public interface IDoor {
        Id<IDoor> Id { get; }
        Id<IDoor> OpposingDoorId { get; }

        Vector2 AnchorLocation { get; }
    }
}