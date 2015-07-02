using UnityEngine;

namespace Assets.Scripts {

    [RequireComponent(typeof (SpriteRenderer))]
    public class SpriteSortingOrderAdjusterBehaviour : MonoBehaviour {

        private SpriteSortingOrderAdjusterBehaviour _spriteSortingOrderAdjuster;
        private SpriteRenderer _spriteRenderer;

        public void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteSortingOrderAdjuster = transform.parent != null
                ? transform.parent.GetComponent<SpriteSortingOrderAdjusterBehaviour>() ?? this
                : this;
        }

        public void Update() {
            if (_spriteSortingOrderAdjuster == this) {
                _spriteRenderer.sortingOrder = -Mathf.RoundToInt(transform.position.y * 1000);
            } else {
                _spriteRenderer.sortingOrder = _spriteSortingOrderAdjuster._spriteRenderer.sortingOrder - 1;
            }
        }
    }
}