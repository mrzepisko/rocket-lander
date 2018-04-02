using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class GameParamsStatic : MonoBehaviour, GameParamsFactory {
        [SerializeField] GameParams parameters;
        public GameParams GetParams() {
            return parameters;
        }
    }
}