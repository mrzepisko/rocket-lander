using System.Collections.Generic;
using UnityEngine;
namespace RocketLander {
    /// <summary>
    /// Default JSON configuration provider. Reads game params from text asset.
    /// </summary>
    public class GameParamsJSON : MonoBehaviour, GameParamsFactory {
        [SerializeField] TextAsset json;

        public virtual GameParams GetParams() {
            return JsonUtility.FromJson<GameParams>(json.text);
        }
    }
}