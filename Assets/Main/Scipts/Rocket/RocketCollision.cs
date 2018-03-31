using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RocketLander {
    public class RocketCollision : MonoBehaviour {
        [SerializeField] float maxSpeed = .5f, landedSpeedTreshold = 1f, touchdownMinTime = 5f;
        [SerializeField] LayerMask deathLayers, platformLayers;
        
#if DEBUG
        public static float relativeForce;
#endif

        //cache
        RocketEngine engine;
        Rigidbody2D rb;

#if DEBUG //very very very ugly but working for debug
        public static
#endif
        //counter
        float touchdown;

        void Awake() {
            engine = GetComponent<RocketEngine>();
            rb = GetComponent<Rigidbody2D>();

            GameEvents.OnStart += EnableOnStart;
        }

        void OnDestroy() {
            GameEvents.OnStart -= EnableOnStart;    
        }

        private void OnEnable() {
            touchdown = 0;
        }


        void OnCollisionEnter2D(Collision2D collision) {
            if (!enabled) return; //high speed crash workaround
            //crashed above speed limit or collided with death layer (water, bounds, etc.)
            if (collision.relativeVelocity.sqrMagnitude > maxSpeed * maxSpeed 
                || ((1 << collision.collider.gameObject.layer) & deathLayers.value) > 0) {
                GameEvents.Crash(new GameEvents.RocketCrash() {
                    position = transform.position,
                    rotation = transform.eulerAngles.z,
                    velocity = collision.relativeVelocity,
                    fuelLeft = engine.FuelLeft,
                });
                enabled = false;
            }

#if DEBUG
            relativeForce = collision.relativeVelocity.magnitude;
#endif
        }

        void OnCollisionStay2D(Collision2D collision) {
            if (!enabled) return; //high speed crash workaround
            //staying on platform layer below speed treshold
            if (((1 << collision.collider.gameObject.layer) & platformLayers.value) > 0
                && collision.relativeVelocity.sqrMagnitude <= landedSpeedTreshold * landedSpeedTreshold
                && Mathf.Abs(collision.otherRigidbody.angularVelocity) < 1) {
                touchdown += Time.fixedDeltaTime;
                if (touchdown >= touchdownMinTime) {
                    GameEvents.RocketLanded(new GameEvents.RocketTouchdown() {
                        fuelLeft = engine.FuelLeft,
                    });
                    enabled = false;
                }
            } else {
                touchdown = 0;
            }
        }

        void OnCollisionExit2D(Collision2D collision) {
            touchdown = 0;
        }
        
        void EnableOnStart() {
            enabled = true;
        }
    }
}