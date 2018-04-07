using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class RocketFX : MonoBehaviour {
        [SerializeField] ParticleSystem smoke, fuel;
        [SerializeField] SpriteRenderer spriteFlame, spriteMonoLeft, spriteMonoRight;
        [SerializeField] AudioClip sfxCrash, sfxContact, sfxFuel;
        [SerializeField] AudioSource audioSource, audioFlame, audioMono;

        RocketEngine engine;

        ParticleSystem.EmissionModule emission;

        void Awake() {
            engine = GetComponent<RocketEngine>();    
        }

        void OnEnable() {
            //cache emission module
            emission = smoke.emission;
            //attach events
            GameEvents.OnRocketCrash += OnCrash;
            GameEvents.OnFuelDepleted += NoFuel;
        }

        void OnDisable() {
            GameEvents.OnRocketCrash -= OnCrash;
            GameEvents.OnFuelDepleted -= NoFuel;
        }

        void OnCrash(GameEvents.RocketCrash crash) {
            audioSource.PlayOneShot(sfxCrash);
        }
        

        void NoFuel() {
            audioSource.PlayOneShot(sfxFuel);
            fuel.Play();
        }

        void Update() {
            //if has fuel and engine is on
            if (engine.ThrottleInput > 0 && engine.FuelLeft > 0) {
                spriteFlame.enabled = true;
                emission.enabled = true;
                if (!audioFlame.isPlaying) {
                    audioFlame.Play();
                }
            } else { //hide flame, stop flame sfx
                spriteFlame.enabled = false;
                emission.enabled = false;
                if (audioFlame.isPlaying) {
                    audioFlame.Stop();
                }
            }

            spriteMonoRight.enabled = engine.TorqueInput < 0;
            spriteMonoLeft.enabled = engine.TorqueInput > 0;

            //mono sfx
            if (spriteMonoRight.enabled || spriteMonoLeft.enabled) {
                if (!audioMono.isPlaying) {
                    audioMono.Play();
                }
            } else {
                audioMono.Stop();   
            }
            
        }

        /// <summary>
        /// Plays contact sfx.
        /// </summary>
        /// <param name="collision"></param>
        void OnCollisionEnter2D(Collision2D collision) {
            audioSource.PlayOneShot(sfxContact);
        }
    }
}