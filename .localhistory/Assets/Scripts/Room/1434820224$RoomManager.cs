using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Util;

namespace Assets.Scripts.Room {
    public class RoomManager {
        
        private readonly IDictionary<Coordinate, IRoom> _roomCache = new Dictionary<Coordinate, IRoom>();
        private readonly IRoomFactory _roomFactory;
        private readonly int _cacheSize;

        public Coordinate CurrentLocation { get; private set; }

        public IRoom CurrentRoom {
            get { return GetOrCreateAndCacheRoom(CurrentLocation); }
        }

        public RoomManager(Coordinate startLocation, IRoomFactory roomFactory, int cacheSize) {
            CurrentLocation = startLocation;
            _roomFactory = roomFactory;
            _cacheSize = cacheSize;

            if (cacheSize > 0) {
                _roomCache.Add(CurrentLocation, CurrentRoom);   
            }
        }

        public void MoveNorth() {
            CurrentLocation = CurrentLocation.North;
        }

        public void MoveEast() {
            CurrentLocation = CurrentLocation.East;
        }

        public void MoveSouth() {
            CurrentLocation = CurrentLocation.South;
        }

        public void MoveWest() {
            CurrentLocation = CurrentLocation.North;
        }

        private IRoom GetOrCreateAndCacheRoom(Coordinate coordinate) {
            IRoom room;
            _roomCache.TryGetValue(coordinate, out room);
            room = room ?? _roomFactory.CreateRoom(coordinate);

            _roomCache.Add(coordinate, room);

            if (_roomCache.Count > _cacheSize) {
                Coordinate furthestAway = CurrentLocation;
                int furthestDistance = 0;
                foreach (var cachedLocation in _roomCache.Keys) {
                    int distance = Math.Abs(coordinate.X - cachedLocation.X) + Math.Abs(coordinate.Y - cachedLocation.Y);
                    if (furthestDistance < distance) {
                        furthestAway = cachedLocation;
                        furthestDistance = distance;
                    }
                }

                _roomCache[furthestAway].Dispose();
                _roomCache.Remove(furthestAway);
            }
            return room;
        }
    }
}
