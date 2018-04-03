﻿using System.Collections;
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

        IEnumerator VerifyDownload(string url) {
            if (url == null || url.Length == 0) {
                InvalidURL();
                yield break;
            }
            using (WWW www = new WWW(url)) {
                while (!www.isDone) {
                    yield return null;
                }
                try {
                    GameParams check = JsonUtility.FromJson<GameParams>(www.text);
                    ValidURL(url);
                } catch {
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