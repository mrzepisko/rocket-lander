using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class MusicSFX : MonoBehaviour {
        [SerializeField] bool persistent = true;
        [SerializeField] AudioClip game, win, intro;
        [SerializeField] AudioSource audioSource, oneShot;

        void Awake() {
            if (persistent) {
                DontDestroyOnLoad(gameObject);
            }
        }

#if UNITY_EDITOR
        void OnValidate() {
            if (Application.isPlaying && persistent) {
                DontDestroyOnLoad(gameObject);
            }
        }
#endif

        void OnDisable() {
            GameEvents.OnRocketTouchdown -= OnTouchdown;
            GameEvents.OnStart -= OnRestart;
        }

        void OnEnable() {
            GameEvents.OnRocketTouchdown += OnTouchdown;
            GameEvents.OnStart += OnRestart;
        }

        void OnRestart() {
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        }

        void OnTouchdown(GameEvents.RocketTouchdown crash) {
            audioSource.Stop();
            oneShot.PlayOneShot(win);
        }
    }
}