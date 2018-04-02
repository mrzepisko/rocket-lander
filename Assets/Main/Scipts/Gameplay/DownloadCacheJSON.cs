using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class DownloadCacheJSON : MonoBehaviour {
        static DownloadCacheJSON instance { get { if (_i == null) Init(); return _i; } }
        static DownloadCacheJSON _i;

        public static string URL { get; set; }
        public static GameParams Default { get; set; }
        public static GameParams Cached { get { return instance.cache; } }

        public delegate void DownloadCompletedAction(bool status, GameParams gameParams);
        public static event DownloadCompletedAction OnDownloadCompleted;

        GameParams cache;

        public static void Reload() {
            instance.Refresh();
        }

        public void Refresh() {
            StartCoroutine(Download());
        }

        static void Init() {
            new GameObject("_downloadCache", typeof(DownloadCacheJSON));
        }

        private void Awake() {
            if (_i == null) {
                _i = this;
                cache = Default;
                DontDestroyOnLoad(gameObject);
                Refresh();
            } else {
                Destroy(gameObject);
            }
        }

        #region events

        private void OnEnable() {
            GameEvents.OnRocketCrash += Refresh;
            GameEvents.OnRocketTouchdown += Refresh;
        }

        private void OnDisable() {
            GameEvents.OnRocketCrash -= Refresh;
            GameEvents.OnRocketTouchdown -= Refresh;
        }

        void Refresh(GameEvents.RocketTouchdown crash) {
            Refresh();
        }

        void Refresh(GameEvents.RocketCrash crash) {
            Refresh();
        }

        #endregion


        IEnumerator Download() {
            using (WWW remote = new WWW(URL)) {
                while (!remote.isDone) {
                    yield return null;
                }
                bool status;
                try {
                    cache = JsonUtility.FromJson<GameParams>(remote.text);
                    status = true;
                } catch {
                    cache = Default;
                    status = false;
                }
                if (OnDownloadCompleted != null) {
                    OnDownloadCompleted(status, cache);
                }
            }
        }
    }
}