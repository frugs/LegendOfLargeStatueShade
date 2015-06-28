using Assets.Scripts.Gameplay.Combat;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player {
    [RequireComponent(typeof (PlayerModelBehaviour))]
    [RequireComponent(typeof (PlayerAnimationBehaviour))]
    [RequireComponent(typeof (HealthBehaviour))]
    public class PlayerBehaviour : MonoBehaviour {
        [SerializeField] private GameObject _swordObject;

        public IPlayerModel PlayerModel { get; private set; }

        public IHealthModel HealthModel { get; private set; }

        public PlayerAnimationBehaviour PlayerAnimation { get; private set; }

        public void Awake() {
            PlayerModel = GetComponent<PlayerModelBehaviour>();
            HealthModel = GetComponent<HealthBehaviour>();
            PlayerAnimation = GetComponent<PlayerAnimationBehaviour>();
        }
    }
}