using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RocketLander {

    public class GameOptionsUI : MonoBehaviour {
        [SerializeField] InputField inputURL;
        [SerializeField] Toggle toggleCustom;
        [SerializeField] Text status;

        Respawner _r;
        Respawner respawner { get { if (_r == null) { _r = FindObjectOfType<Respawner>(); } return _r; } }

        void OnEnable() {
            respawner.gameObject.SetActive(false);
        }

        void OnDisable() {
            //exit check
            if (respawner != null) {
                respawner.gameObject.SetActive(true);
            }
        }

        public void VerifyURL() {
            StartCoroutine(VerifyDownload(inputURL.text));
        }

        /// <summary>
        /// Check provided url if contains valid json data for configuration.
        /// </summary>
        /// <param name="url">target json</param>
        /// <returns></returns>
        IEnumerator VerifyDownload(string url) {
            //invalid url 
            if (string.IsNullOrEmpty(url)) {
                InvalidURL();
                yield break;
            }
            bool valid = false;
            try {
                //begin download
                using (WWW www = new WWW(url)) {
                    //wait until done
                    while (!www.isDone) {
                        yield return null;
                    }
                    //check if valid response
                    if (!string.IsNullOrEmpty(www.text)) {
                        GameParams check = JsonUtility.FromJson<GameParams>(www.text);
                        valid = true;
                    }
                }
            } finally {
                //valid json
                if (valid) {
                    ValidURL(url);
                } else { //invalid url - error occured in any step
                    InvalidURL();
                }
            }
        }

        public void OnToggleCustom(bool value) {
            if (!value) {
                DownloadCacheJSON.ResetURL();
                DownloadCacheJSON.Reload();
            }
        }

        void InvalidURL() {
            ColorBlock colors = inputURL.colors;
            colors.normalColor = Color.red;
            inputURL.colors = colors;
            DownloadCacheJSON.ResetURL();
            DownloadCacheJSON.Reload();
        }

        void ValidURL(string url) {
            ColorBlock colors = inputURL.colors;
            colors.normalColor = Color.white;
            inputURL.colors = colors;
            DownloadCacheJSON.URL = url;
            DownloadCacheJSON.Reload();
        }
    }
}