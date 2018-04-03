using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class RocketFX : MonoBehaviour {
        [SerializeField] ParticleSystem smoke;
        [SerializeField] SpriteRenderer spriteFlame, spriteMonoLeft, spriteMonoRight;
        RocketEngine engine;

        ParticleSystem.EmissionModule emission;

        private void Awake() {
            engine = GetComponent<RocketEngine>();    
        }

        void OnEnable() {
            emission = smoke.emission;
        }

        private void Update() {
            if (engine.ThrottleInput > 0 && engine.FuelLeft > 0) {
                spriteFlame.enabled = true;
                emission.enabled = true;
            } else {
                spriteFlame.enabled = false;
                emission.enabled = false;
            }

            spriteMonoRight.enabled = engine.TorqueInput < 0;
            spriteMonoLeft.enabled = engine.TorqueInput > 0;

        }


    }
}