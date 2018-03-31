
namespace RocketLander {
    /// <summary>
    /// Default gameplay data.
    /// </summary>
    [System.Serializable]
    public struct GameParams {
        public float gravity, fuel, thrust, torque, mass;
    }

    /// <summary>
    /// Interface for delivering game parameters.
    /// </summary>
    public interface GameParamsFactory {
        GameParams GetParams();
    }
}