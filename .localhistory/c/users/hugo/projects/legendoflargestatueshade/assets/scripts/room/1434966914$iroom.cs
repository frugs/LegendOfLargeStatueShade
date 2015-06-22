using System;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room {
    public interface IRoom : IDisposable {
        Id<IRoom> Id { get; }
        Vector2 Size { get; }
        Vector2 Centre { get; }
    }
}