using UnityEngine;

namespace Assets.Scripts.MainMenu {
    public class StartButtonBehaviour : MonoBehaviour {

        // Called from button press
        public void StartGame() {
            Application.LoadLevel("scene");
        }
    }
}