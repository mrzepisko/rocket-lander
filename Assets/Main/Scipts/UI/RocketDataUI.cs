using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace RocketLander {
    public class RocketDataUI : MonoBehaviour {
#if UI
        const string SPEED_FORMAT_X = "x: {0:+0.00;-0.00}", SPEED_FORMAT_Y = "y: {0:+0.00;-0.00}"; 
        //references
        [SerializeField] Text speedHorizontal, speedVertical;
        [SerializeField] Slider fuel;

        //cache
        RocketEngine engine;

        void OnEnable() {
            if (engine == null) {
                engine = FindObjectOfType<RocketEngine>();
                if (engine == null) {
                    gameObject.SetActive(false);
                    Debug.LogWarningFormat("Rocket not found, disabling {0}", name);
                    return;
                }
            }
        }

        void LateUpdate() {
            speedHorizontal.text = string.Format(SPEED_FORMAT_X, engine.Velocity.x);
            speedVertical.text = string.Format(SPEED_FORMAT_Y, engine.Velocity.y);
            fuel.value = engine.FuelLeft / engine.MaxFuel;
        }

        public void RestartAttempt() {
            GameEvents.Restart();
        }
#endif
    }
}