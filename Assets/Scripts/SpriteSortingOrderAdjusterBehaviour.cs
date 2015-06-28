using UnityEngine;

namespace Assets.Scripts {
    
    [RequireComponent(typeof (SpriteRenderer))]
    public class SpriteSortingOrderAdjusterBehaviour : MonoBehaviour {

        public void Update() {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -Mathf.RoundToInt(transform.position.y * 100);
        }
    }
}