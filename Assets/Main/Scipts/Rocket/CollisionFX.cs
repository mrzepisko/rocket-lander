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

        /// <summary>
        /// Emit crash particles at given position.
        /// </summary>
        /// <param name="crash">crash event info</param>
        void Emit(GameEvents.RocketCrash crash) {
            transform.position = crash.position;
            transform.rotation = Quaternion.AngleAxis(crash.rotation, Vector3.forward);
            crashParticles.Clear(); //hide old crash particles
            crashParticles.Play();
        }
    }
}