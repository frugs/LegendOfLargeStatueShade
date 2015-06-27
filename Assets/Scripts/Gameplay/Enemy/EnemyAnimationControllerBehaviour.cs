using System;
using System.Collections;
using Assets.Scripts.Gameplay.Combat;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Enemy {
    [RequireComponent(typeof(EnemyBehaviour), typeof(Animator), typeof(SpriteRenderer))]
    public class EnemyAnimationControllerBehaviour : MonoBehaviour {
        private EnemyBehaviour _enemyBehaviour;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        public Action DeathAnimationFinished { get; set; }

        public void Awake() {
            _enemyBehaviour = GetComponent<EnemyBehaviour>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            DeathAnimationFinished = () => { };
        }

        public void Start() {
            _enemyBehaviour.HealthModel.HurtBy += PlayHurt;
            _enemyBehaviour.HealthModel.Killed += PlayKilled;
        }

        public void OnDestroy() {
            _enemyBehaviour.HealthModel.HurtBy -= PlayHurt;
            _enemyBehaviour.HealthModel.Killed += PlayKilled;
        }

        private void PlayHurt(DamageBehaviour damage) {
            StartCoroutine(AnimationUtil.FlashColourCoroutine(_spriteRenderer, Color.red, 0.5f));
            _animator.SetTrigger("DoHurt");
        }

        private void PlayKilled(DamageBehaviour damage) {
            StartCoroutine(KilledCoroutine());
        }

        private IEnumerator KilledCoroutine() {
            foreach (var obj in AnimationUtil.FadeOutCoroutine(_spriteRenderer, 0.5f)) {
                yield return obj;
            }
            DeathAnimationFinished();
        }
    }
}