using System;

namespace Assets.Scripts.Gameplay.Enemy {
    public interface IEnemyModel {

        Action Attack { get; }
    }
}