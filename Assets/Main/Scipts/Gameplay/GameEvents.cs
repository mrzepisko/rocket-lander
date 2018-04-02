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
        public static event NotifyAction OnStart;

        public static void Crash(RocketCrash crash) {
            if (OnRocketCrash != null) {
                OnRocketCrash(crash);
            }
        }

        public static void RocketLanded(RocketTouchdown info) {
            if (OnRocketTouchdown != null) {
                OnRocketTouchdown(info);
            }
            PersistentData.AddLanded();
        }

        public static void Restart() {
            if (OnRestart != null) {
                OnRestart();
            }
        }

        public static void Start() {
            if (OnStart != null) {
                OnStart();
            }
        }

        public struct RocketTouchdown {
            public float fuelLeft;
        }

        public struct RocketCrash {
            public Vector3 position;
            public float rotation;
            public Vector3 velocity;
            public float fuelLeft;
            public float overkill;
        }
    }
}