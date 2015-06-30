using System;
using Assets.Scripts.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts.Room {
    public abstract class AreaTransitionBehaviour : MonoBehaviour {
        public abstract RoomBehaviour Room { get; }
        public abstract AreaTransitionBehaviour OpposingAreaTransition { get; }

        public abstract Vector2 ExitDirection { get; }

        public abstract Action<PlayerBehaviour, AreaTransitionBehaviour> PlayerExitThroughAreaTransition { get; set; }
        public abstract Action<AreaTransitionBehaviour> OldAreaFadeOutFinished { get; set; }
        public abstract Action<AreaTransitionBehaviour> NewAreaFadeInFinished { get; set; }
        public abstract Action<PlayerBehaviour, AreaTransitionBehaviour> PlayerEntryThroughAreaTransition { get; set; }
    }
}