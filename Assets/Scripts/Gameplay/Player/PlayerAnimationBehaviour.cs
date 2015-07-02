using System;
using Assets.Scripts.Gameplay.Combat;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player {
    [RequireComponent(typeof (Rigidbody2D), typeof (PlayerBehaviour), typeof (Animator))]
    [RequireComponent(typeof (SpriteRenderer))]
    public class PlayerAnimationBehaviour : MonoBehaviour {
        private IPlayerModel _playerModel;
        private IHealthModel _healthModel;

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidBody2D;

        public Action ReturnedToIdle { get; set; }

        public void Awake() {
            var player = GetComponent<PlayerBehaviour>();
            _playerModel = player.PlayerModel;
            _healthModel = player.HealthModel;

            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidBody2D = GetComponent<Rigidbody2D>();

            ReturnedToIdle = () => { };
        }

        public void Start() {
            _playerModel.DoAttack += PlayAttack;
            _playerModel.DoJump += PlayJump;
            _healthModel.HurtBy += PlayHurt;
        }

        public void OnDestroy() {
            _playerModel.DoAttack -= PlayAttack;
            _playerModel.DoJump -= PlayJump;
            _healthModel.HurtBy -= PlayHurt;
        }

        public void Update() {
            _animator.SetInteger("direction", CalculateDirection(_playerModel.FacingDirection));
            _animator.SetFloat("velocity", _rigidBody2D.velocity.magnitude);
        }

        private static int CalculateDirection(Direction facingDirection) {
            switch (facingDirection) {
                case Direction.Up:
                    return 0;
                case Direction.Right:
                    return 1;
                case Direction.Down:
                    return 2;
                case Direction.Left:
                    return 3;
            }
            return 0;
        }

        private void PlayAttack() {
            _animator.SetTrigger("attack");
        }

        private void PlayJump() {
            _animator.SetTrigger("jump");
        }

        private void PlayHurt(DamageBehaviour damage) {
            StartCoroutine(AnimationUtil.FlashColourCoroutine(_spriteRenderer, Color.red, 0.5f));
        }

        // Called from animation
        private void ReturnToIdle() {
            ReturnedToIdle();
        }
    }
}