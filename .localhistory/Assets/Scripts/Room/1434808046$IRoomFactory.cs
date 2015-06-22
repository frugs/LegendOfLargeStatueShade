﻿using Assets.Scripts.Util;

namespace Assets.Scripts.Room {
    public interface IRoomFactory {
        IRoom CreateRoom(Coordinate location);
    }
}
