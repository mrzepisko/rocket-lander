using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class DownloadCacheJSON : MonoBehaviour {
        const string VALID_URL_REGEX = @"/((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/";
        static DownloadCacheJSON instance { get { if (_i == null) Init(); return _i; } }
        static DownloadCacheJSON _i;

        public static string URL { get { return _url; } set { if (defaultURL == null) defaultURL = value; _url = value; } }
        public static DownloadStatus Status { get { return _status;  } }
        public static GameParams Default { get; set; }
        public static GameParams Cached { get { return instance.cache; } }

        public delegate void DownloadCompletedAction(bool status, GameParams gameParams);
        public static event DownloadCompletedAction OnDownloadCompleted;

        static string _url;
        static string defaultURL;
        static DownloadStatus _status = DownloadStatus.Downloading;

        GameParams cache;

        public static void Reload() {
            instance.Refresh();
        }

        public void Refresh() {
            _status = DownloadStatus.Downloading;
            StartCoroutine(Download(URL));
        }

        static void Init() {
            new GameObject("_downloadCache", typeof(DownloadCacheJSON));
        }

        public static void ResetURL() {
            _url = defaultURL;
        }

        void Awake() {
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

        void OnEnable() {
            GameEvents.OnRocketCrash += Refresh;
            GameEvents.OnRocketTouchdown += Refresh;
        }

        void OnDisable() {
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


        IEnumerator Download(string url) {
            bool status = false;
            try {
                //check for valid url
                if (!string.IsNullOrEmpty(url)) {
                    using (WWW remote = new WWW(url)) {
                        //wait until ready
                        while (!remote.isDone) {
                            yield return null;
                        }
                        if (!string.IsNullOrEmpty(remote.text)) {
                            //parse to json
                            cache = JsonUtility.FromJson<GameParams>(remote.text);
                            status = true;
                        }
                    }
                }
            } finally { //check status, send notify
                if (!status) {
                    cache = Default;
                    _status = DownloadStatus.Error;
                } else {
                    _status = DownloadStatus.Ready;
                }

                if (OnDownloadCompleted != null) {
                    OnDownloadCompleted(status, cache);
                }
            }
        }
    }
    public enum DownloadStatus {
        Downloading,
        Ready,
        Error,
    }
}