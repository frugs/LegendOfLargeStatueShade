using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player {
    [RequireComponent(typeof (PlayerModelBehaviour))]
    [RequireComponent(typeof(PlayerAnimationBehaviour))]
    public class PlayerBehaviour : MonoBehaviour {
        [SerializeField] private GameObject _swordObject;

        private bool _isAttacking;
        private float _facingDirection;
        private PlayerModelBehaviour _playerModel;
        private PlayerAnimationBehaviour _playerAnimation;

        public IPlayerModel PlayerModel {
            get { return _playerModel; }
        }

        public PlayerAnimationBehaviour PlayerAnimation {
            get { return _playerAnimation; }
        }

        public void Awake() {
            _playerModel = GetComponent<PlayerModelBehaviour>();
            _playerAnimation = GetComponent<PlayerAnimationBehaviour>();
        }
    }
}