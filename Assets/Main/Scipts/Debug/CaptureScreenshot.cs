#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketLander {
    public class CaptureScreenshot : MonoBehaviour {
        public void Capture() {
            string path = UnityEditor.EditorUtility.SaveFilePanelInProject("Save as...", "Screenshot", "png", "Save screenshot");
            if (path != null && path != "") {
                ScreenCapture.CaptureScreenshot(path);
            }
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.H)) {
                Capture();
            }
        }
    }
}
#endif