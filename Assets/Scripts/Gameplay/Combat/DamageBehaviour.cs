using UnityEngine;

namespace Assets.Scripts.Gameplay.Combat {
    /// <summary>
    /// <para>
    /// This behaviour requires a kinematic rigidbody and a collider in order
    /// for it to be subject to collision and trigger events independently
    /// of the parents of the gameobject it is attached to.
    /// </para>
    /// </summary>
    [RequireComponent(typeof (Rigidbody2D), typeof (Collider2D))]
    public class DamageBehaviour : MonoBehaviour {
        [SerializeField] private DamageType _damageType;
        [SerializeField] private float _damage;

        public float Damage {
            get { return _damage; }
        }

        public DamageType DamageType {
            get { return _damageType; }
        }
    }
}