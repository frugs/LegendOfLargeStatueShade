using System;
using System.Collections.Generic;
using Assets.Scripts.Util;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomFactory : IRoomFactory {

        private readonly IDictionary<Id<IRoom>, DefaultRoomData> _rooms; 

        public IRoom CreateRoom(Id<IRoom> roomId) {
            throw new NotImplementedException();
        }

        public IRoom CreateAdjacentRoom(IDoor door) {
            throw new NotImplementedException();
        }
    }
}