using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Util {
    public class AnimationUtil {
        private AnimationUtil() {}

        /// <summary>
        /// <para>
        /// This is the coroutine assumes that the original colour of the colour filter
        /// was  white, and will set the colour filter to white upon completion.
        /// </para>
        /// </summary>
        public static IEnumerator FlashColourCoroutine(SpriteRenderer sprite, Color colour, float smoothTime) {
            for (var t = 0f; t <= smoothTime / 2; t += Time.deltaTime) {
                sprite.color = Color.Lerp(sprite.color, colour, t);
                yield return null;
            }

            for (var t = 0f; t <= smoothTime / 2; t += Time.deltaTime) {
                sprite.color = Color.Lerp(sprite.color, Color.white, t);
                yield return null;
            }

            sprite.color = Color.white;
        }

        public static IEnumerable FadeOutCoroutine(SpriteRenderer sprite, float smoothTime) {
            for (var t = 0f; t <= smoothTime / 2; t += Time.deltaTime) {
                sprite.color = Color.Lerp(sprite.color, new Color(1, 1, 1, 0), t);
                yield return null;
            }
        }
    }
}