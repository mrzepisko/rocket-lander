
using System.Collections;
using UnityEngine;
namespace RocketLander {
    public class GameParamsRemoteJSON : GameParamsJSON {
        const string DEFAULT_URL = "http://cyberpho.com/dev/rl/custom.js";
        const string PREFS_CACHE_VERSION = "params_version";
        [SerializeField] string url = DEFAULT_URL;
        

        private void Awake() {
            DownloadCacheJSON.URL = url;
        }

        private void Start() {
            DownloadCacheJSON.Reload();
        }

        public override GameParams GetParams() {
            return DownloadCacheJSON.Cached;
        }

#if UNITY_EDITOR
        private void OnValidate() {
            if (Application.isPlaying) {
                DownloadCacheJSON.URL = url;
                DownloadCacheJSON.Default = base.GetParams();
            }
        }
#endif
    }

}