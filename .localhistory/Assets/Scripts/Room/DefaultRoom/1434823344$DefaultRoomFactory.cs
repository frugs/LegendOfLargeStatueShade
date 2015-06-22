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

        private readonly IList<DefaultRoomData> _rooms = new List<DefaultRoomData>();


        public void Awake() {
            Id<IDoor> door1Id = new Id<IDoor>(new GUID());
            

            AddRoomData(new DefaultRoomData(new Id<IRoom>(new GUID()),
                                            new Vector2(200, 200), 
                                            _room1Prefab,
                                            new List<DefaultDoorData> {
                                                
                                            }));
        }

        private void AddRoomData(DefaultRoomData defaultRoomData) {
            _rooms.Add(defaultRoomData);

        }

        public IRoom CreateRoom(Id<IRoom> roomId) {
            throw new NotImplementedException();
        }

        public IRoom CreateAdjacentRoom(IDoor door) {
            throw new NotImplementedException();
        }
    }
}