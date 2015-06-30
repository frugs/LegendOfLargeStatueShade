using System;

namespace Assets.Scripts.Room {
    public interface ICurrentRoomModel {
        RoomBehaviour CurrentRoom { get; }

        Action<RoomBehaviour, RoomBehaviour> CurrentRoomUpdated { get; set; }
    }
}