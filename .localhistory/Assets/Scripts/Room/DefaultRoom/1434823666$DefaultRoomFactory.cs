using System;
using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomFactoryBehaviour : MonoBehaviour, IRoomFactory {
        
        [SerializeField]
        private GameObject _room1Prefab;

        private readonly IDictionary<Id<IRoom>, DefaultRoomData> _rooms = new Dictionary<Id<IRoom>, DefaultRoomData>();
        private readonly IDictionary<Id<IDoor>, Id<IRoom>> _doorIndex = new Dictionary<Id<IDoor>, Id<IRoom>>(); 

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
                                            _room1Prefab,
                                            new List<DefaultDoorData> {
                                                door2
                                            }));
        }

        private void AddRoomData(DefaultRoomData defaultRoomData) {
            _rooms.Add(defaultRoomData.Id, defaultRoomData);

        }

        public IRoom CreateRoom(Id<IRoom> roomId) {
            throw new NotImplementedException();
        }

        public IRoom CreateAdjacentRoom(IDoor door) {
            throw new NotImplementedException();
        }
    }
}