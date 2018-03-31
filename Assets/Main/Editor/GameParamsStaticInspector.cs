using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RocketLander;
using UnityEditor;
using System.IO;

namespace RocketEditor {
    [CustomEditor(typeof(GameParamsStatic))]
    [CanEditMultipleObjects]
    public class GameParamsStaticInspector : Editor {
        bool export = false;
        bool format = false;
        string path;
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            export = EditorGUILayout.Foldout(export, "Export");
            if (export) {
                format = GUILayout.Toggle(format, "Format?");
                if (GUILayout.Button("Save to JSON")) {
                    path = EditorUtility.SaveFilePanelInProject("Save...", "GameParams", "json", "Save as json");
                    ToJson(target as GameParamsStatic, path, format);
                }
            }
        }

        void ToJson(GameParamsFactory factory, string path, bool format = false) {
            File.WriteAllText(path, JsonUtility.ToJson(factory.GetParams(), format));
        }
    }
}