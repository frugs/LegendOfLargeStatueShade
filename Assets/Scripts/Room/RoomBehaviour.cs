﻿using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room {
    using System.Collections.ObjectModel;

    public abstract class RoomBehaviour : MonoBehaviour {
        abstract public Id<RoomBehaviour> Id { get; }
        abstract public Vector2 Size { get; }
        abstract public Vector2 Centre { get; }

        public abstract ReadOnlyCollection<DoorBehaviour> Doors { get; }
        public abstract DoorBehaviour GetDoor(Id<DoorBehaviour> doorId);

        abstract public void Activate();
        abstract public void Deactivate();
    }
}