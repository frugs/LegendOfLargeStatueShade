using System;
using UnityEngine;

namespace Assets.Scripts.Room {
    public interface IRoom : IDisposable {
        Vector2 SizeInWorldUnits { get; }
        Vector2 CentreInWorldCoords { get; }
    }
}
