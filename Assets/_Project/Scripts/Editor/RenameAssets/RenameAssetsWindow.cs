using PainfulSmile.Editor.Core;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PainfulSmile.Editor.RenameAssets
{
    public class RenameAssetsWindow : EditorWindow
    {
        public enum Types
        {
            Texture,
            Material,
            Model
        }

        private static Types assetType;
        private static string referenceName;
        private static string newName;

        [MenuItem(PainfulSmileEditorKeys.ToolsPath + "Rename Assets")]
        private static void CreateWindow()
        {
            RenameAssetsWindow window = CreateInstance<RenameAssetsWindow>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 300);

            referenceName = PlayerPrefs.GetString(PainfulSmileEditorKeys.RenameAssets.ReferenceNameKey);
            newName = PlayerPrefs.GetString(PainfulSmileEditorKeys.RenameAssets.NewNameKey);

            window.Show();
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(referenceName) || string.IsNullOrWhiteSpace(referenceName))
                referenceName = PlayerPrefs.GetString(PainfulSmileEditorKeys.RenameAssets.ReferenceNameKey);
        }

        private static void ReplaceNames()
        {
            PlayerPrefs.SetString(PainfulSmileEditorKeys.RenameAssets.ReferenceNameKey, referenceName);
            PlayerPrefs.SetString(PainfulSmileEditorKeys.RenameAssets.NewNameKey, newName);
            PlayerPrefs.Save();

            foreach (var asset in AssetDatabase.FindAssets("t: " + assetType.ToString()))
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);

                if (path.Contains(referenceName))
                {
                    var fileName = Path.GetFileNameWithoutExtension(path);

                    while (fileName.Contains(referenceName))
                    {
                        fileName = fileName.Replace(referenceName, newName);
                    }

                    AssetDatabase.RenameAsset(path, fileName);
                }
            }

            AssetDatabase.Refresh();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.HelpBox("Enter the type of the asset: ", MessageType.None);

            assetType = (Types)EditorGUILayout.EnumPopup(assetType, options: null);

            EditorGUILayout.HelpBox("Enter the name that you want replace: ", MessageType.None);
            referenceName = EditorGUILayout.TextField("Name: ", referenceName);

            EditorGUILayout.HelpBox("Enter the new value of what will be replace: ", MessageType.None);
            newName = EditorGUILayout.TextField("new Name: ", newName);

            EditorGUILayout.HelpBox("MAKE SURE YOU MAKE A BACKUP OR UPLOAD IT TO GIT BEFORE RENAMING, THIS IS IRREVERSIBLE.", MessageType.Warning);

            if (GUILayout.Button("Generate"))
            {
                ReplaceNames();
            }

            EditorGUILayout.EndVertical();
        }
    }
}