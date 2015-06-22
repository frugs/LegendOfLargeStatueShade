using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room {
    // TODO: This should probably be an abstract subclass of MonoBehaviour
    public interface IDoor {
        Id<IDoor> Id { get; }
        Id<IDoor> OpposingDoorId { get; }

        Vector2 AnchorLocation { get; }
    }
}