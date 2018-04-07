using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RocketLander {
    /// <summary>
    /// Simple keyboard control implementation.
    /// </summary>
    public class RocketInputKeyboard : RocketInput {
#if UNITY_EDITOR
        RocketEngine _re;

        RocketEngine engine { get { if (_re == null) { _re = GetComponent<RocketEngine>(); } return _re; } }

        void Update() {
            if (!AllowInput) return;
            engine.TorqueInput = Input.GetAxisRaw("Horizontal");
            engine.ThrottleInput = Input.GetAxisRaw("Vertical");
        }
#endif
    }
}