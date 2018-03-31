using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class RocketFX : MonoBehaviour {
        [SerializeField] ParticleSystem smoke;
        [SerializeField] SpriteRenderer spriteFlame;
        RocketEngine engine;

        private void Awake() {
            engine = GetComponent<RocketEngine>();    
        }

        private void Update() {
            spriteFlame.enabled = engine.ThrottleInput > 0;
        }


    }
}