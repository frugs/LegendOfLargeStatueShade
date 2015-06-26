
namespace Assets.Scripts.Room {
    using DefaultRoom;
    using Tiled2Unity;
    using UnityEngine;
    
    /// <summary>
    /// <para>
    /// This class is dependent on having prefab instances of maps imported from the
    /// Tiled Map Editor using Tiled2Unity as a decendant of the gameobject this behaviour 
    /// is attached to.
    /// </para>
    /// </summary>
    public class TiledRoomBehaviour : DefaultRoomBehaviour {

        public override Rect Area {
            get {
                var tiledMap = GetComponentsInChildren<TiledMap>(true)[0];

                return new Rect(tiledMap.transform.position.x,
                    tiledMap.transform.position.y - tiledMap.NumTilesHigh,
                    tiledMap.NumTilesWide,
                    tiledMap.NumTilesHigh);
            }
        }
    }
}
