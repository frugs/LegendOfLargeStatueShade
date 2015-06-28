using System;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player {
    [RequireComponent(typeof (Rigidbody2D), typeof (PlayerAnimationBehaviour))]
    public class PlayerModelBehaviour : MonoBehaviour, IPlayerModel {
        private const float PlayerSpeed = 4f;

        private PlayerAnimationBehaviour _playerAnimation;
        private PlayerControls _playerControlsForFrame = new PlayerControls(Vector2.zero, false);
        private Rigidbody2D _rigidBody2D;

        public bool IsIdle { get; private set; }
        public Direction FacingDirection { get; private set; }

        public IPlayerController PlayerController { get; set; }
        
        public Action DoAttack { get; set; }

        public void Awake() {
            IsIdle = true;
            _playerAnimation = GetComponent<PlayerAnimationBehaviour>();
            PlayerController = InputPlayerController.GetInputPlayerController();
            FacingDirection = Direction.Down;
            DoAttack = () => {
                IsIdle = false;
                _rigidBody2D.velocity = Vector2.zero;
            };
        }

        public void Start() {
            _rigidBody2D = GetComponent<Rigidbody2D>();         
            _playerAnimation.ReturnedToIdle += UpdateIdle;
        }

        public void Destroy() {
            _playerAnimation.ReturnedToIdle -= UpdateIdle;
        }

        public void Update() {
            _playerControlsForFrame = PlayerController.ControlPlayer();
            if (IsIdle) {
                if (_playerControlsForFrame.ShouldAttack) {
                    DoAttack();
                }

                if (_playerControlsForFrame.MovementDirection.magnitude > 0) {
                    FacingDirection = DirectionUtil.FromVector2(_playerControlsForFrame.MovementDirection);                    
                }
            }
        }

        public void FixedUpdate() {
            if (IsIdle) {
                _rigidBody2D.velocity = _playerControlsForFrame.MovementDirection * PlayerSpeed;
            }
        }

        private void UpdateIdle() {
            IsIdle = true;
        }
    }
}