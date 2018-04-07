#if DEBUG && UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    [ExecuteInEditMode]
    public class ShowDot : MonoBehaviour {
        [SerializeField] Vector3 world = Vector3.up;
        [SerializeField] float dot;
        void Update() {
            dot = Vector3.Dot(world, transform.up);
        }
    }
}
#endif