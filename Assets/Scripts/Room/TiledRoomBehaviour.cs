using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tiled2Unity;
using UnityEngine;

namespace Assets.Scripts.Room {
    /// <summary>
    /// <para>
    /// This class is dependent on having prefab instances of maps imported from the
    /// Tiled Map Editor using Tiled2Unity as a decendant of the gameobject this behaviour 
    /// is attached to.
    /// </para>
    /// </summary>
    public class TiledRoomBehaviour : RoomBehaviour {
        public override Rect Area {
            get {
                var tiledMap = GetComponentsInChildren<TiledMap>(true)[0];

                return new Rect(tiledMap.transform.position.x,
                    tiledMap.transform.position.y - tiledMap.NumTilesHigh,
                    tiledMap.NumTilesWide,
                    tiledMap.NumTilesHigh);
            }
        }

        public override ReadOnlyCollection<DoorBehaviour> Doors {
            get {
                var doors = gameObject.GetComponentsInChildren<DoorBehaviour>(true);
                return new List<DoorBehaviour>(doors).AsReadOnly();
            }
        }

        public override void Activate() {
            gameObject.SetActive(true);
        }

        public override void Deactivate() {
            gameObject.SetActive(false);
        }

        public void Awake() {
            Deactivate();
        }
    }
}