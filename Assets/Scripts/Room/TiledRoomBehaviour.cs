
namespace Assets.Scripts.Room {
    using DefaultRoom;
    using Tiled2Unity;
    using UnityEngine;

    public class TiledRoomBehaviour : DefaultRoomBehaviour {

        public override Rect Area {
            get {
                var tiledMap = GetComponentInChildren<TiledMap>();

                return new Rect(tiledMap.transform.position.x,
                    tiledMap.transform.position.y - tiledMap.NumTilesHigh,
                    tiledMap.NumTilesWide,
                    tiledMap.NumTilesHigh);
            }
        }
    }
}
