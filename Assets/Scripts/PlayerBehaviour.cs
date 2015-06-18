﻿using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts {

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerBehaviour : MonoBehaviour {

        [SerializeField]
        private float _playerSpeed = 1f;

        [SerializeField]
        private float _attackTimeoutLength = 0.25f;

        [SerializeField]
        private GameObject _swordObject;

        private Animator _animator;
        private Rigidbody2D _rigidBody2D;

        private bool _isAttacking;
        private float _facingDirection;

        public void Start() {
            _animator = GetComponent<Animator>();
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _swordObject.SetActive(false);
        }

        public void Update() {
            if (!_isAttacking) {
                _rigidBody2D.velocity = new Vector2(Input.GetAxis(InputMappings.Horizontal) * _playerSpeed, Input.GetAxis(InputMappings.Vertical) * _playerSpeed);

                if (Input.GetButtonDown(InputMappings.Attack)) {
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

        private void UpdateFacingDirection() {
            Vector2 velocity = _rigidBody2D.velocity;
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
