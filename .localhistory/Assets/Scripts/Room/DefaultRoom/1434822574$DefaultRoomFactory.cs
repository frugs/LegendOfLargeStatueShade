﻿using System;
using System.Collections.Generic;
using Assets.Scripts.Util;

namespace Assets.Scripts.Room.DefaultRoom {
    public class DefaultRoomFactoryBehaviour : IRoomFactory {

        private readonly IList<DefaultRoomData> _rooms = new List<DefaultRoomData>(); 

        public IRoom CreateRoom(Id<IRoom> roomId) {
            throw new NotImplementedException();
        }

        public IRoom CreateAdjacentRoom(IDoor door) {
            throw new NotImplementedException();
        }
    }
}