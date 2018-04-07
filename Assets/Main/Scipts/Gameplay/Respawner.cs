using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    /// <summary>
    /// Manages rocket respawning.
    /// </summary>
    public class Respawner : MonoBehaviour {
        [SerializeField] float dropDelay;
        [SerializeField] float randomPosition, randomRotation;
        [SerializeField] float dropHeight;

        Rigidbody2D rb;
        RocketEngine engine;

        Vector3 dropCenter;

        void Awake() {
            //cache references
            rb = GetComponent<Rigidbody2D>();
            engine = GetComponent<RocketEngine>();

            //set drop vector
            dropCenter = Vector3.up * dropHeight;
        }
        

        void OnEnable() {
            //attach events
            GameEvents.OnRestart += Respawn;
            GameEvents.OnRocketCrash += OnCrash;
            GameEvents.OnRocketTouchdown += OnTouchdown;
            Respawn();
        }

        void OnTouchdown(GameEvents.RocketTouchdown crash) {
            //delay respawn
            Invoke("Respawn", dropDelay);
        }

        void OnCrash(GameEvents.RocketCrash crash) {
            Respawn();
        }

        void OnDisable() {
            GameEvents.OnRestart -= Respawn;
        }

        public void Respawn() {
            //cancel 'rocket ready', prepare it again
            CancelInvoke("DelayDrop");
            //reset physics
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            //set new location
            engine.transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(-randomRotation, randomRotation, Random.value), Vector3.forward);
            engine.transform.position = dropCenter + Vector3.Lerp(Vector3.left, Vector3.right, Random.value) * randomPosition;

            //freeze until released
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //reset controls
            RocketInput.AllowInput = false;
            engine.ThrottleInput = 0;
            engine.TorqueInput = 0;

            //delay
            Invoke("DelayDrop", dropDelay);
        }

        void DelayDrop() {
            //refresh params from config
            engine.RefreshConfig();
            //unfreeze
            rb.constraints = RigidbodyConstraints2D.None;
            RocketInput.AllowInput = true;
            //modify score
            PersistentData.AddAttempt();
            //notify
            GameEvents.Start();
        }

#if DEBUG
        void OnDrawGizmosSelected() {
            Gizmos.color = new Color(1, .5f, 0);
            Gizmos.DrawLine(Vector3.up * dropHeight - Vector3.right * randomPosition, Vector3.up * dropHeight + Vector3.right * randomPosition);
            Gizmos.DrawLine(Vector3.up * dropHeight - Vector3.right * randomPosition + Vector3.up, Vector3.up * dropHeight - Vector3.right * randomPosition - Vector3.up);
            Gizmos.DrawLine(Vector3.up * dropHeight + Vector3.right * randomPosition + Vector3.up, Vector3.up * dropHeight + Vector3.right * randomPosition - Vector3.up);
        }
#endif
    }
}