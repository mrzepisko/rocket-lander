using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RocketLander {
    public class GameParamsUI : MonoBehaviour {
        [SerializeField] Text gravity, fuel, thrust, torque, mass, downloadStatus;

        GameParams target;

        private void OnEnable() {
            downloadStatus.text = "";
            DownloadCacheJSON.OnDownloadCompleted += OnDownloadComplete;
            Download();
        }

        private void OnDisable() {
            DownloadCacheJSON.OnDownloadCompleted -= OnDownloadComplete;
        }

        private void OnDownloadComplete(bool status, GameParams gameParams) {
            downloadStatus.color = status ? Color.green : Color.red;
            downloadStatus.text = status ? "Completed" : "Download failed";
            target = gameParams;
            RefreshUI();
        }

        public void Reload() {
            Download();
        }

        void Download() {
            DownloadCacheJSON.Reload();
            downloadStatus.text = "Waiting...";
            downloadStatus.color = Color.yellow;
        }

        void RefreshUI() {
            gravity.text = string.Format("Gravity:\t{0:0.0}", target.gravity);
            fuel.text = string.Format("Fuel:\t{0:0.0}", target.fuel);
            thrust.text = string.Format("Thrust:\t{0:0.0}", target.thrust);
            torque.text = string.Format("Torque:\t{0:0.0}", target.torque);
            mass.text = string.Format("Mass:\t{0:0.0}", target.mass);
        }
    }
}