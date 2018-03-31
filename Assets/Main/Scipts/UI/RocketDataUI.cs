using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace RocketLander {
    public class RocketDataUI : MonoBehaviour {
#if UI
        const string SPEED_FORMAT_X = "x: {0:+0.00;-0.00}", SPEED_FORMAT_Y = "y: {0:+0.00;-0.00}", ATTEMPTS_FORMAT = "score: {0}/{1}"; 
        //references
        [SerializeField] Text speedHorizontal, 
            speedVertical, attempts, crashInfo;
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

            GameEvents.OnRocketCrash += OnCrash;
            GameEvents.OnStart += OnStart;
        }

        void OnStart() {
            attempts.text = string.Format(ATTEMPTS_FORMAT, PersistentData.Landed, PersistentData.Total);
        }

        void OnDisable() {
            GameEvents.OnRocketCrash -= OnCrash;
        }

        void OnCrash(GameEvents.RocketCrash crash) {
            CancelInvoke("HideCrashInfo");
            crashInfo.text = string.Format("CRASH! Relative velocity: {0:0.00}", crash.velocity.magnitude);
            Invoke("HideCrashInfo", 2f);
        }

        void HideCrashInfo() {
            crashInfo.text = "";
        }

        void LateUpdate() {
            speedHorizontal.text = string.Format(SPEED_FORMAT_X, engine.Velocity.x);
            speedVertical.text = string.Format(SPEED_FORMAT_Y, engine.Velocity.y);
            fuel.value = engine.FuelLeft / engine.MaxFuel;
        }

        public void RestartAttempt() {
            GameEvents.Restart();
        }

        public void WipeData() {
            PersistentData.Wipe();
        }

        
#endif
    }
}