using System;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player {
    [RequireComponent(typeof (Rigidbody2D), typeof (PlayerBehaviour), typeof (Animator))]
    public class PlayerAnimationBehaviour : MonoBehaviour {
        private Animator _animator;
        private IPlayerModel _playerModel;
        private Rigidbody2D _rigidBody2D;

        public Action ReturnedToIdle { get; set; }

        public void Awake() {
            _animator = GetComponent<Animator>();
            _playerModel = GetComponent<PlayerBehaviour>().PlayerModel;
            _rigidBody2D = GetComponent<Rigidbody2D>();
            ReturnedToIdle = () => { };
        }

        public void Start() {
            _playerModel.DoAttack += PlayAttack;
        }

        public void OnDestroy() {
            _playerModel.DoAttack -= PlayAttack;
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

        // Called from animation
        private void ReturnToIdle() {
            ReturnedToIdle();
        }
    }
}