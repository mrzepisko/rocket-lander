using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class Respawner : MonoBehaviour {
        [SerializeField] float dropDelay;
        [SerializeField] float randomPosition, randomRotation;
        [SerializeField] float dropHeight;

        Rigidbody2D rb;
        RocketEngine engine;

        Vector3 dropCenter;

        void Awake() {
            rb = GetComponent<Rigidbody2D>();
            engine = GetComponent<RocketEngine>();
        }

        void Start() {
            dropCenter = Vector3.up * dropHeight;
            Respawn();
        }

        void OnEnable() {
            GameEvents.OnRestart += Respawn;
            GameEvents.OnRocketCrash += OnCrash;
            GameEvents.OnRocketTouchdown += OnTouchdown;
        }

        private void OnTouchdown(GameEvents.RocketTouchdown crash) {
            Invoke("Respawn", dropDelay);
        }

        private void OnCrash(GameEvents.RocketCrash crash) {
            Respawn();
        }

        void OnDisable() {
            GameEvents.OnRestart -= Respawn;
        }

        public void Respawn() {
            CancelInvoke("DelayDrop");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            engine.transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(-randomRotation, randomRotation, Random.value), Vector3.forward);
            engine.transform.position = dropCenter + Vector3.Lerp(Vector3.left, Vector3.right, Random.value) * randomPosition;

            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            RocketInput.AllowInput = false;
            engine.ThrottleInput = 0;
            engine.TorqueInput = 0;

            Invoke("DelayDrop", dropDelay);
        }

        void DelayDrop() {
            engine.RefreshConfig();
            rb.constraints = RigidbodyConstraints2D.None;
            RocketInput.AllowInput = true;
            PersistentData.AddAttempt();
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