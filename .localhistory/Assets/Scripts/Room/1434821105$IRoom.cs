using System;
using Assets.Scripts.Util;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Room {
    public interface IRoom : IDisposable {
        Id<IRoom> Id { get; }
        Vector2 SizeInWorldUnits { get; }
        Vector2 CentreInWorldCoords { get; }
    }
}