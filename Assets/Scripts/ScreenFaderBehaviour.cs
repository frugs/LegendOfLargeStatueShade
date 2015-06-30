using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class ScreenFaderBehaviour : MonoBehaviour {
        [SerializeField] private Image _background;

        public void Awake() {
            _background.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        }

        public IEnumerable FadeToColour(Color colour) {
            var elapsedTime = 0f;
            var time = 0.75f;

            while (elapsedTime < time) {
                _background.color = Color.Lerp(_background.color, colour, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}