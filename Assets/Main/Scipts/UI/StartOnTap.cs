
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RocketLander {
    /// <summary>
    /// Intro skipping.
    /// </summary>
    public class StartOnTap : MonoBehaviour {
        [SerializeField] int sceneId;
        void Update() {
            if (Input.touchCount > 0 || Input.anyKeyDown) {
                SceneManager.LoadScene(sceneId);
            }
        }
    }
}