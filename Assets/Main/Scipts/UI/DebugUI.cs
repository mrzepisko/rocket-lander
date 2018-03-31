using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace RocketLander {
    [DefaultExecutionOrder(200000)]
    public class DebugUI : MonoBehaviour {
#if DEBUG
        Rigidbody2D rocket;

        [SerializeField] Text collisionForce;

        private void Start() {
            rocket = FindObjectOfType<RocketEngine>().GetComponent<Rigidbody2D>();
        }

        private void OnGUI() {
            Rect rect = new Rect(0, Screen.height - 20, Screen.width, 20);
            GUI.color = Color.red;
            GUI.Label(rect, string.Format("Last collision: {0:0.0000}", RocketCollision.relativeForce.ToString()));
            rect.y -= 20f; 
            GUI.Label(rect, string.Format("Angular speed: {0:0.0000}", rocket.angularVelocity));
            rect.y -= 20f;
            GUI.Label(rect, string.Format("Speed: {0:0.0000}", rocket.velocity.magnitude));
            rect.y -= 20f;
            GUI.Label(rect, string.Format("Touchdown: {0:0.0000}s", RocketCollision.touchdown));
        }
#else
        private void Awake() {
            Destroy(gameObject);
        }
#endif
    }
}