using System;
using System.Collections;
using Assets.Scripts.Gameplay.Combat;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Enemy {
    [RequireComponent(typeof(EnemyBehaviour), typeof(Animator), typeof(SpriteRenderer))]
    public class EnemyAnimationControllerBehaviour : MonoBehaviour {
        private EnemyBehaviour _enemyBehaviour;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        public void Awake() {
            _enemyBehaviour = GetComponent<EnemyBehaviour>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Start() {
            _enemyBehaviour.HealthModel.HurtBy += PlayHurt;
        }

        public void OnDestroy() {
            _enemyBehaviour.HealthModel.HurtBy -= PlayHurt;
        }

        private void PlayHurt(DamageBehaviour damage) {
            StartCoroutine(FlashRed());
            _animator.SetTrigger("DoHurt");
        }

        private IEnumerator FlashRed() {
            for (var t = 0f; t <= 1f; t += 0.1f) {
                _spriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
                yield return null;
            }
            
            for (var t = 0f; t <= 1f; t += 0.1f) {
                _spriteRenderer.color = Color.Lerp(Color.red, Color.white, t);
                yield return null;
            }

			_spriteRenderer.color = Color.white;
        }
    }
}