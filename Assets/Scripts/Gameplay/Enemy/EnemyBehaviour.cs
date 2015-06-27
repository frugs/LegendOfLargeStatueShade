using Assets.Scripts.Gameplay.Combat;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Enemy {

    [RequireComponent(typeof(HealthBehaviour))]
    public class EnemyBehaviour : MonoBehaviour {
        public IHealthModel HealthModel { get { return GetComponent<HealthBehaviour>(); }}
    }
}