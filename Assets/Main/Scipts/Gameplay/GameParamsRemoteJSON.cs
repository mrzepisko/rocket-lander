
using System.Collections;
using UnityEngine;
namespace RocketLander {
    public class GameParamsRemoteJSON : GameParamsJSON {
        const string DEFAULT_URL = "";
        const string PREFS_CACHE_VERSION = "params_version";
        [SerializeField] string url = DEFAULT_URL;
        

        private void Awake() {
        }

        private void Start() {
            DownloadCacheJSON.Reload();
        }

        public override GameParams GetParams() {
            if (DownloadCacheJSON.Status.Equals(DownloadStatus.Ready)) {
                Debug.Log("Loading remote JSON");
                return DownloadCacheJSON.Cached;
            } else {
                Debug.Log("Loading default JSON");
                return base.GetParams();
            }
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