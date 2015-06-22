using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room {
    // TODO: This should probably be an abstract subclass of MonoBehaviour
    public interface IRoom {
        Id<IRoom> Id { get; }
        Vector2 Size { get; }
        Vector2 Centre { get; }

        void Activate();
        void Deactivate();
    }
}