
namespace Assets.Scripts.Player {
    using UnityEngine;

    public class InputPlayerController : IPlayerController {
        public PlayerControls ControlPlayer() {
            var movementDirection = new Vector2(Input.GetAxis(InputMappings.Horizontal), Input.GetAxis(InputMappings.Vertical));
            return new PlayerControls(movementDirection, Input.GetButtonDown(InputMappings.Attack));
        }
    }
}
