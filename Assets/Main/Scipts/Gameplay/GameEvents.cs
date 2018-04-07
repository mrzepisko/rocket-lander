using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class GameEvents {
        public delegate void RocketCrashAction(RocketCrash crash);
        public delegate void RocketTouchdownAction(RocketTouchdown crash);
        public delegate void NotifyAction();

        public static event RocketCrashAction OnRocketCrash;
        public static event RocketTouchdownAction OnRocketTouchdown;
        public static event NotifyAction OnRestart;
        public static event NotifyAction OnFuelDepleted;
        public static event NotifyAction OnStart;

        /// <summary>
        /// Trigger event rocket crashed.
        /// </summary>
        /// <param name="crash">crash info</param>
        public static void Crash(RocketCrash crash) {
            if (OnRocketCrash != null) {
                OnRocketCrash(crash);
            }
        }

        /// <summary>
        /// Trigger event rocket landed.
        /// </summary>
        /// <param name="info">landing info</param>
        public static void RocketLanded(RocketTouchdown info) {
            if (OnRocketTouchdown != null) {
                OnRocketTouchdown(info);
            }
            PersistentData.AddLanded();
        }

        /// <summary>
        /// Trigger event restart game.
        /// </summary>
        public static void Restart() {
            if (OnRestart != null) {
                OnRestart();
            }
        }

        /// <summary>
        /// Trigger event start game.
        /// </summary>
        public static void Start() {
            if (OnStart != null) {
                OnStart();
            }
        }

        /// <summary>
        /// Trigger event no fuel left.
        /// </summary>
        public static void FuelDepleted() {
            if (OnFuelDepleted != null) {
                OnFuelDepleted();
            }
        }

        /// <summary>
        /// Event data struct.
        /// </summary>
        public struct RocketTouchdown {
            public float fuelLeft;
        }

        /// <summary>
        /// Event data struct.
        /// </summary>
        public struct RocketCrash {
            public Vector3 position;
            public float rotation;
            public Vector3 velocity;
            public float fuelLeft;
            public float overkill;
        }
    }
}