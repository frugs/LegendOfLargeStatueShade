using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class ScreenFaderBehaviour : MonoBehaviour {
        [SerializeField] private Image _background;

        private IEnumerator _fadeRoutine;

        public bool IsFadedOut { get; private set; }
        public Action FadedOut { get; set; }
        public Action FadedIn { get; set; }

        public void Awake() {
            _background.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
            FadedOut = () => { };
            FadedIn = () => { };
        }

        public void FadeOut() {
            if (_fadeRoutine != null) {
                StopCoroutine(_fadeRoutine);
            }

            _fadeRoutine = FadeOutCoroutine();
            StartCoroutine(_fadeRoutine);
        }

        public void FadeIn() {
            if (_fadeRoutine != null) {
                StopCoroutine(_fadeRoutine);
            }

            _fadeRoutine = FadeInCoroutine();
            StartCoroutine(_fadeRoutine);
        }

        private IEnumerator FadeOutCoroutine() {
            foreach (var frame in FadeToColour(Color.black)) {
                yield return frame;
            }
            FadedOut();
            FadedOut = () => { };
            _fadeRoutine = null;
            IsFadedOut = true;
        }

        private IEnumerator FadeInCoroutine() {
            IsFadedOut = false;
            foreach (var frame in FadeToColour(Color.clear)) {
                yield return frame;
            }
            FadedIn();
            FadedIn = () => { };
            _fadeRoutine = null;
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