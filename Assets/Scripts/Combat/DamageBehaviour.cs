using UnityEngine;

namespace Assets.Scripts.Combat {
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