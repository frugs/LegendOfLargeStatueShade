﻿using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Util;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomFactoryBehaviour : MonoBehaviour, IRoomFactory {

        private static readonly GUID Door1Guid = new GUID("9d7272d1-535a-44b4-a732-a943929f9a48");
        private static readonly GUID Door2Guid = new GUID("a3aa9d93-2560-48ce-9cb3-306ff8b1b91e");

        private readonly IDictionary<Id<IRoom>, GameObject> _rooms = new Dictionary<Id<IRoom>, GameObject>();
        private readonly IDictionary<Id<IDoor>, Id<IRoom>> _doorIndex = new Dictionary<Id<IDoor>, Id<IRoom>>();

        [SerializeField] private GameObject _room1Prefab;

        [SerializeField] private GameObject _room2Prefab;

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

            return new DefaultRoom(roomId, _room1Prefab);
        }

        private void AlignRoom(DefaultRoom adjacentRoom, IDoor door) {
            var adjacentDoor = adjacentRoom.Doors.Single(roomDoor => door.OpposingDoorId.Equals(roomDoor.Id));
            var delta = adjacentDoor.AnchorLocation - door.AnchorLocation;
            adjacentRoom.GameObject.transform.Translate(delta);
        }
    }
}