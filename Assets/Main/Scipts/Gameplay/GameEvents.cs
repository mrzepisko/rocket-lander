using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class GameEvents {
        public delegate void RocketCrashAction(RocketCrash crash);
        public delegate void NotifyAction();

        public static event RocketCrashAction OnRocketCrash;
        public static event NotifyAction OnRestart;

        public static void Crash(RocketCrash crash) {
            Restart();
            if (OnRocketCrash != null) {
                OnRocketCrash(crash);
            }
        }

        public static void Restart() {
            if (OnRestart != null) {
                OnRestart();
            }
        }


        public struct RocketCrash {
            public Vector3 position;
            public float rotation;
            public Vector3 velocity;
            public float fuelLeft;
        }
    }
}