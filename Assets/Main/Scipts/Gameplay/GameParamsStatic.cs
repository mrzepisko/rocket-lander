using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    /// <summary>
    /// Provides static param configuration.
    /// </summary>
    public class GameParamsStatic : MonoBehaviour, GameParamsFactory {
        [SerializeField] GameParams parameters;
        public GameParams GetParams() {
            return parameters;
        }
    }
}