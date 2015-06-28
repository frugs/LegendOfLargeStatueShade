using System;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player {
    public interface IPlayerModel {
        bool IsIdle { get; }
        Direction FacingDirection { get; }

        IPlayerController PlayerController { get; set; }
        Action DoAttack { set; get; }
    }
}