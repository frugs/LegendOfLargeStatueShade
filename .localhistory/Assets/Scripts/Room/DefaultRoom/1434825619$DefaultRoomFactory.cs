﻿using System;
using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomFactoryBehaviour : MonoBehaviour, IRoomFactory {
        
        private readonly IDictionary<Id<IRoom>, DefaultRoomData> _rooms = new Dictionary<Id<IRoom>, DefaultRoomData>();
        private readonly IDictionary<Id<IDoor>, Id<IRoom>> _doorIndex = new Dictionary<Id<IDoor>, Id<IRoom>>();

        [SerializeField]
        private GameObject _room1Prefab;

        [SerializeField]
        private GameObject _room2Prefab;

        public void Awake() {
            var door1 = new DefaultDoorData(new Id<IDoor>(new GUID()), 
                                            new Id<IDoor>(new GUID()),
                                            new Vector2(-100, 0));
            var door2 = new DefaultDoorData(door1.OpposingDoorId,
                                            door1.Id,
                                            new Vector2(50, 0));

            AddRoomData(new DefaultRoomData(new Id<IRoom>(new GUID()),
                                            new Vector2(200, 200), 
                                            _room1Prefab,
                                            new List<DefaultDoorData> {
                                                door1
                                            }));
            AddRoomData(new DefaultRoomData(new Id<IRoom>(new GUID()),
                                            new Vector2(100, 500),
                                            _room2Prefab,
                                            new List<DefaultDoorData> {
                                                door2
                                            }));
        }

        private void AddRoomData(DefaultRoomData defaultRoomData) {
            _rooms.Add(defaultRoomData.Id, defaultRoomData);
            foreach (var door in defaultRoomData.Doors) {
                _doorIndex.Add(door.Id, defaultRoomData.Id);
            }
        }

        public IRoom CreateRoom(Id<IRoom> roomId) {
            var room = CreateDefaultRoom(roomId);
            room.GameObject.SetActive(true);
            return room;
        }

        public IRoom CreateAdjacentRoom(IDoor door) {
            var adjacentRoom = CreateDefaultRoom(_doorIndex[door.OpposingDoorId]);
            AlignRoom(adjacentRoom, door);
            return adjacentRoom;
        }

        private DefaultRoom CreateDefaultRoom(Id<IRoom> roomId) {
            var roomData = _rooms[roomId];

            return new DefaultRoom(roomId, roomData.Size, _room1Prefab, roomData.Doors);
        }

        private void AlignRoom(DefaultRoom adjacentRoom, IDoor door) {
            adjacentRoom.Doors
        }
    }

    internal class DefaultDoor : IDoor {
        private readonly Id<IDoor> _id;
        private readonly Id<IDoor> _opposingDoorId;
        private readonly Vector2 _anchorLocationInRoomSpace;
        private readonly DefaultRoom _defaultRoom;

        public DefaultDoor(Id<IDoor> id, Id<IDoor> opposingDoorId, Vector2 anchorLocationInRoomSpace, DefaultRoom defaultRoom) {
            _id = id;
            _opposingDoorId = opposingDoorId;
            _anchorLocationInRoomSpace = anchorLocationInRoomSpace;
            _defaultRoom = defaultRoom;
        }

        public Id<IDoor> Id {
            get { return _id; }
        }

        public Id<IDoor> OpposingDoorId {
            get { return _opposingDoorId; }
        }

        public Vector2 AnchorLocation {
            get { return _anchorLocationInRoomSpace; }
        }
    }
}