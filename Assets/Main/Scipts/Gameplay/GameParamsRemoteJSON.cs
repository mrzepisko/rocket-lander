
using System.Collections;
using UnityEngine;
namespace RocketLander {
    /// <summary>
    /// Downloads game params as custom json from remote url.
    /// </summary>
    public class GameParamsRemoteJSON : GameParamsJSON {
        const string DEFAULT_URL = "";
        const string PREFS_CACHE_VERSION = "params_version";
        [SerializeField] string url = DEFAULT_URL;
        
        void Start() {
            DownloadCacheJSON.Reload();
        }

        public override GameParams GetParams() {
            //get remote params if downloaded, otherwise use provided text asset
            if (DownloadCacheJSON.Status.Equals(DownloadStatus.Ready)) {
                return DownloadCacheJSON.Cached;
            } else {
                return base.GetParams();
            }
        }

        void OnValidate() {
            if (Application.isPlaying) {
                DownloadCacheJSON.URL = url;
                DownloadCacheJSON.Default = base.GetParams();
            }
        }
    }

}