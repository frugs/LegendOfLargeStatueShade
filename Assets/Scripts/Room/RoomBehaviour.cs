using UnityEngine;

namespace Assets.Scripts.Room {
    using System.Collections.ObjectModel;

    public abstract class RoomBehaviour : MonoBehaviour {
        abstract public Rect Area { get; }

        public abstract ReadOnlyCollection<DoorBehaviour> Doors { get; }
        public abstract ReadOnlyCollection<AreaTransitionBehaviour> AreaTransitions { get; }

        abstract public void Activate();
        abstract public void Deactivate();
    }
}