using System;
using UnityEngine;

namespace Assets.Scripts.Combat {
    [RequireComponent(typeof (Collider2D))]
    public class HealthBehaviour : MonoBehaviour {
        private const float DefaultMaxHealth = 5f;

        [SerializeField] private float _maxHealth = DefaultMaxHealth;
        [SerializeField] private float _health;

        [SerializeField] private DamageVulnerabilityBehaviour _damageVulnerability;

        public Action<DamageBehaviour> HurtBy { get; set; }
        public Action<DamageBehaviour> Killed { get; set; }

        public float Health {
            get { return _health; }
        }

        public void Awake() {
            Action<DamageBehaviour> doNothing = damage => { };
            HurtBy = doNothing;
            Killed = doNothing;
        }

        public void OnTriggerEnter2D(Collider2D other) {
            var damage = other.gameObject.GetComponentInChildren<DamageBehaviour>();
            if (damage != null && _damageVulnerability.IsVulnerableTo(damage)) {
                _health -= damage.Damage;
                HurtBy(damage);
            }
        }
    }
}