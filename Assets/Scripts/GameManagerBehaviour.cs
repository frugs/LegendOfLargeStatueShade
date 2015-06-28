using System;
using Assets.Scripts.Gameplay.Combat;
using Assets.Scripts.Gameplay.Player;
using UnityEngine;

namespace Assets.Scripts {
    public class GameManagerBehaviour : MonoBehaviour {
        [SerializeField] private PlayerBehaviour _player;

        public void Start() {
            _player.HealthModel.Killed += GameOver;
        }

        public void OnDestroy() {
            _player.HealthModel.Killed -= GameOver;
        }

        private void GameOver(DamageBehaviour obj) {
            Application.LoadLevel("MainMenu");
        }
    }
}