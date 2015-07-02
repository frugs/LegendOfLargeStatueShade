using Assets.Scripts.Util;
using System;

namespace Assets.Scripts.Gameplay.Player {
    public interface IPlayerModel {
        bool IsIdle { get; }
        bool IsJumping { get; }
        Direction FacingDirection { get; }

        IPlayerController PlayerController { get; set; }
        Action DoAttack { set; get; }
        Action DoJump { set; get; }
    }
}