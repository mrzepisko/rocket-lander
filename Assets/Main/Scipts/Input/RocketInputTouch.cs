using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    /// <summary>
    /// Controls for touch input.
    /// </summary>
    public class RocketInputTouch : RocketInput {
        [Tooltip("Screen height in pixels below touch registers as control input")]
        [SerializeField]int maxHeight = 400;
        [SerializeField] [Range(0, 1f)] float center = .5f;

        RocketEngine _re;
        RocketEngine engine { get { if (_re == null) { _re = GetComponent<RocketEngine>(); } return _re; } }

        void Start() { }
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
        void Update() {
            if (!AllowInput) return;
            float throttle = 0;
            float torqueLeft = 0, torqueRight = 0;

            if (Input.touchCount > 0) {
                for (int i = 0; i < Input.touchCount; i++) {
                    Vector3 touchPosition = Input.GetTouch(i).position;
                    if (touchPosition.y > maxHeight) { //ignore over height limit
                        continue;
                    }
                    float x = Camera.main.ScreenToViewportPoint(touchPosition).x;
                    if (x >= center) { //thrust
                        throttle = 1f;
                    } else if (x < (1f - center) / 2f) { //torque left
                        torqueLeft = -1f;
                    } else { //torque right
                        torqueRight = 1f;
                    }
                }
            }

            engine.ThrottleInput = throttle;
            engine.TorqueInput = torqueLeft + torqueRight;
        }
#endif
    }
}