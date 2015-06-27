using System;

namespace Assets.Scripts.Gameplay.Combat {
    public interface IHealthModel {
        Action<DamageBehaviour> HurtBy { get; set; }
        Action<DamageBehaviour> Killed { get; set; }

        float Health { get; }

        void Activate();
        void Deactivate();
    }
}