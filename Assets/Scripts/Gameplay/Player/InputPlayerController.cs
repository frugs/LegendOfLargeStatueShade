
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player {
    public class InputPlayerController : IPlayerController {
        private static readonly InputPlayerController Instance = new InputPlayerController();

        public static InputPlayerController GetInputPlayerController() {
            return Instance;
        }

        public PlayerControls ControlPlayer() {
            var movementDirection = new Vector2(Input.GetAxis(InputMappings.Horizontal), Input.GetAxis(InputMappings.Vertical));
            return new PlayerControls(movementDirection, Input.GetButtonDown(InputMappings.Attack));
        }
    }
}
