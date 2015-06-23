namespace Assets.Scripts.Player {
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof (Rigidbody2D))]
    [RequireComponent(typeof (Animator))]
    public class PlayerBehaviour : MonoBehaviour {
        private static readonly IPlayerController DefaultPlayerController = new InputPlayerController();

        [SerializeField] private float _playerSpeed = 4f;

        [SerializeField] private float _attackTimeoutLength = 0.25f;

        [SerializeField] private GameObject _swordObject;

        private Animator _animator;
        private Rigidbody2D _rigidBody2D;

        private PlayerControls _playerControlsForFrame = new PlayerControls(Vector2.zero, false);

        private bool _isAttacking;
        private float _facingDirection;

        public IPlayerController PlayerController { get; set; }

        public void Start() {
            _animator = GetComponent<Animator>();
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _swordObject.SetActive(false);
            SetDefaultPlayerController();
        }

        public void Update() {
            _playerControlsForFrame = PlayerController.ControlPlayer();
        }

        public void FixedUpdate() {
            if (!_isAttacking) {
                _rigidBody2D.velocity = _playerControlsForFrame.MovementDirection * _playerSpeed;

                if (_playerControlsForFrame.ShouldAttack) {
                    StartAttacking();
                    StartCoroutine(AttackTimeout());
                }
            }

            UpdateFacingDirection();
            _swordObject.transform.eulerAngles = new Vector3(0, 0, _facingDirection);

            _animator.SetFloat("velocityX", _rigidBody2D.velocity.x);
            _animator.SetFloat("velocityY", _rigidBody2D.velocity.y);
            _animator.SetBool("isAttacking", _isAttacking);
        }

        public void SetDefaultPlayerController() {
            PlayerController = DefaultPlayerController;
        }

        private void UpdateFacingDirection() {
            var velocity = _rigidBody2D.velocity;
            if (velocity.y > 0) {
                _facingDirection = 180;
            } else if (velocity.y < 0) {
                _facingDirection = 0;
            } else if (velocity.x > 0) {
                _facingDirection = 90;
            } else if (velocity.x < 0) {
                _facingDirection = 270;
            }
        }

        private void StartAttacking() {
            _rigidBody2D.velocity = Vector2.zero;
            _isAttacking = true;
            _swordObject.SetActive(true);
        }

        // Called from animator
        public void StopAttacking() {
            _isAttacking = false;
            _swordObject.SetActive(false);
        }

        private IEnumerator AttackTimeout() {
            yield return new WaitForSeconds(_attackTimeoutLength);
            StopAttacking();
        }
    }
}