using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class CollisionFX : MonoBehaviour {
        [SerializeField] int emitCount = 30;
        [SerializeField] ParticleSystem crashParticles;

        void OnEnable() {
            GameEvents.OnRocketCrash += Emit;
        }

        void OnDisable() {
            GameEvents.OnRocketCrash -= Emit;
        }

        void Emit(GameEvents.RocketCrash crash) {
            transform.position = crash.position;
            transform.rotation = Quaternion.AngleAxis(crash.rotation, Vector3.forward);
            crashParticles.Clear();
            crashParticles.Play();
        }
    }
}