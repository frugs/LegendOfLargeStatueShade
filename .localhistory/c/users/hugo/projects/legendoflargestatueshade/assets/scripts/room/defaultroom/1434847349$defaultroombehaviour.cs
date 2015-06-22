﻿using System;
using System.Collections.ObjectModel;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomBehaviour : MonoBehaviour, IRoom {
        [SerializeField] private Id<IRoom> _id;

        [SerializeField] private Vector2 _roomSize;

        public void Awake() {}

        public Vector2 RoomSize {
            get { return _roomSize; }
        }

        public ReadOnlyCollection<IDoor> Doors {
            get {
                gameObject.GetComponents<DefaultDoorBehaviour>()
            }
        }

        public Id<IRoom> Id {
            get { return _id; }
        }

        public Vector2 Size {
            get { return _roomSize; }
        }

        public Vector2 Centre { get; private set; }

        public void SetDoorIds(int doorIndex, Id<IDoor> doorId, Id<IDoor> oppositeId) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            Destroy(gameObject);
        }
    }

    public class DefaultDoorBehaviour : MonoBehaviour, IDoor {}
}