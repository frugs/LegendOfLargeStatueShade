using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player {
    public class SwordBehaviour : MonoBehaviour {
        [SerializeField] private PlayerBehaviour _playerBehaviour;

        public void Start() {
            DeactivateSword();
            _playerBehaviour.PlayerModel.DoAttack += ActivateSword;
            _playerBehaviour.PlayerAnimation.ReturnedToIdle += DeactivateSword;
        }

        public void Destroy() {
            _playerBehaviour.PlayerModel.DoAttack -= ActivateSword;
            _playerBehaviour.PlayerAnimation.ReturnedToIdle -= DeactivateSword;
        }

        public void Update() {
            var facingDirection = _playerBehaviour.PlayerModel.FacingDirection;
            transform.eulerAngles = new Vector3(0, 0, CalculateRotation(facingDirection));
        }

        private static int CalculateRotation(Direction dir) {
            switch (dir) {
                case Direction.Up:
                    return 180;
                case Direction.Right:
                    return 90;
                case Direction.Down:
                    return 0;
                case Direction.Left:
                    return 270;
            }
            return 180;
        }

        private void ActivateSword() {
            gameObject.SetActive(true);
        }

        private void DeactivateSword() {
            gameObject.SetActive(false);
        }
    }
}