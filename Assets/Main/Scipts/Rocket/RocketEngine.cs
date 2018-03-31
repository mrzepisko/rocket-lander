using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    [RequireComponent(typeof(Rigidbody2D))]
    public class RocketEngine : MonoBehaviour {
        //rocket specific
        [SerializeField] float engineOffset;

        //public access
        public float ThrottleInput { get { return throttleInput; } set { throttleInput = Mathf.Clamp01(value); } }
        public float TorqueInput { get { return torqueInput; } set { torqueInput = Mathf.Clamp(value, -1f, 1f); } }
        public Vector3 ThrustVector { get { return thrustVector; } }
        public Vector3 Velocity { get { return rb.velocity; } }
        public float FuelLeft { get { return fuel; } }
        public float MaxFuel { get { return maxFuel; } }
        //component cache
        Rigidbody2D rb;
        GameParamsFactory config;

        //runtime variables
        float throttleInput, torqueInput;
        Vector3 thrustVector;

        //global config
        float thrust, torque, fuel, maxFuel;

        /// <summary>
        /// Engine position in world coordinates.
        /// </summary>
        Vector3 enginePosition { get { return transform.TransformPoint(Vector3.down * engineOffset); } }

        void Awake() {
            config = GetComponent<GameParamsFactory>();
            if (config == null) {
                enabled = false;
                Debug.LogError("Rocket configuration not found");
                return;
            }
            rb = GetComponent<Rigidbody2D>();
        }

        void OnEnable() {
            RefreshConfig();
        }

        void FixedUpdate() {
            //1 unit of fuel per second
            float fuelConsumption = ThrottleInput * Time.fixedDeltaTime;
            //sufficient fuel
            if (fuel >= fuelConsumption) {
                fuel = Mathf.MoveTowards(fuel, 0, fuelConsumption);
                thrustVector = transform.up * ThrottleInput * thrust;
            } else if (fuel > 0) { //fuel too low to burn on 100% efficiency
                thrustVector = transform.up * (fuel / fuelConsumption) * thrust;
                fuel = 0;
            } else { //no fuel
                thrustVector = Vector3.zero;
            }
            rb.AddForceAtPosition(thrustVector * Time.fixedDeltaTime, enginePosition);
            rb.AddTorque(-TorqueInput * torque * Time.fixedDeltaTime);
        }


        public void RefreshConfig() {
            GameParams gameParams = config.GetParams();
            rb.mass = gameParams.mass;
            thrust = gameParams.thrust;
            torque = gameParams.torque;
            maxFuel = fuel = gameParams.fuel;
            Physics2D.gravity = Vector3.down * gameParams.gravity;
        }

#if DEBUG
        void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(enginePosition, .1f);
        }
#endif
    }
}