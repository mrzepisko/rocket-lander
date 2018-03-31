using UnityEngine;
namespace RocketLander {
    public class GameParamsJSON : MonoBehaviour, GameParamsFactory {
        [SerializeField] TextAsset json;

        public GameParams GetParams() {
            return JsonUtility.FromJson<GameParams>(json.text);
        }
    }
}