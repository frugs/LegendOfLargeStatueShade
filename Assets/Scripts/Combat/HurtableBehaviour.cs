using UnityEngine;

namespace Assets.Scripts.Combat {
    [RequireComponent(typeof (Collider2D))]
    public class HurtableBehaviour : MonoBehaviour {
        public float Health { get; private set; }

        public void OnTriggerEnter2D(Collider2D other) {}
    }
}