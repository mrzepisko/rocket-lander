using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RocketLander {
    public class RocketCollision : MonoBehaviour {
        [SerializeField] float maxSpeed = .5f;
        [SerializeField] LayerMask deathLayers, platformLayers;

        //cache
        RocketEngine engine;

        void Awake() {
            engine = GetComponent<RocketEngine>();
        }

        void OnCollisionEnter2D(Collision2D collision) {
            //crashed above speed limit or collided with death layer (water, bounds, etc.)
            if (collision.relativeVelocity.sqrMagnitude > maxSpeed * maxSpeed 
                || (collision.otherCollider.gameObject.layer & deathLayers.value) > 0) {
                GameEvents.Crash(new GameEvents.RocketCrash() {
                    position = transform.position,
                    rotation = transform.eulerAngles.z,
                    velocity = collision.relativeVelocity,
                    fuelLeft = engine.FuelLeft,
                });
            }
        }
    }
}