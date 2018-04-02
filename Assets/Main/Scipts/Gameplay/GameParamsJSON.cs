using System.Collections.Generic;
using UnityEngine;
namespace RocketLander {
    public class GameParamsJSON : MonoBehaviour, GameParamsFactory {
        [SerializeField] TextAsset json;

        public virtual GameParams GetParams() {
            return JsonUtility.FromJson<GameParams>(json.text);
        }
    }
}