using Assets.Scripts.Gameplay.Combat;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Enemy {

    [RequireComponent(typeof(HealthBehaviour), typeof(EnemyAnimationControllerBehaviour))]
    public class EnemyBehaviour : MonoBehaviour {
        private EnemyAnimationControllerBehaviour _enemyAnimationController;

        public IHealthModel HealthModel {
            get { return GetComponent<HealthBehaviour>(); }
        }

        public void Start() {
            _enemyAnimationController = GetComponent<EnemyAnimationControllerBehaviour>();
            _enemyAnimationController.DeathAnimationFinished += DestroySelf;
        }

        public void OnDestroy() {
            _enemyAnimationController.DeathAnimationFinished -= DestroySelf;
        }

        private void DestroySelf() {
            Destroy(gameObject);
        }
    }
}