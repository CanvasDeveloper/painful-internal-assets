using PainfulSmile.Editor.Core;
using UnityEditor;
using UnityEngine;

namespace PainfulSmile.Editor.TemplateCreator
{
    public class CreateFolderStructure : EditorWindow
    {
        private static string rootFolderName = "_Project";
        private static bool createGitPlaceholder = true;

        [MenuItem(PainfulSmileEditorKeys.ToolsPath + "Create Default Folders")]
        private static void CreateWindow()
        {
            CreateFolderStructure window = CreateInstance<CreateFolderStructure>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 300);
            window.Show();
        }

        private static void CreateFolders()
        {
            /// ---- How To Use ---- \\\
            /// First, create your main folder like so:
            /// VirtualFolder rootFolder = new VirtualFolder("Assets", rootFolderName);
            /// This will create a folder with whatever name you entered in the window, which you can find in Unity under Assets -> Create Default Folders.
            /// In order to add sub folders to this, you simply call either AddSubFolder or AddSubFolders on it, like so:
            /// rootFolder.AddSubFolder("YourNewSubFolder");
            /// This will add a new folder into the root folder, which ultimately looks like this: Assets/RootFolder/YourNewSubFolder.
            /// You can call AddSubFolder again to add yet another sub folder to THAT sub folder, like this:
            /// rootFolder.AddSubFolder("YourNewSubFolder").AddSubFolder("YetAnotherFolder");
            /// Which will result in this structure: Assets/RootFolder/YourNewSubFolder/YetAnotherFolder.
            /// You can chain these as much as you'd like.
            /// Furthermore, you can add multiple folders at once by calling AddSubFolders, like this:
            /// rootFolder.AddSubFolders(new string[] {"FolderOne", "FolderTwo", "FolderThree"});
            /// Note that this does NOT return the folders, so you can't chain on these. 
            /// So you should probably only do this on the deepest part of your hierachy.
            /// Finally, below is an example implementation that will probably suit most people just fine.
            /// But feel free to change it to your liking.


            const string ASSETS_FOLDER = "Assets";
            const string NO_VERSION_CONTROL_FOLDER = "__NoVersionControl";
            const string AUDIO_FOLDER = "Audio";
            const string SCRIPTS_FOLDER = "Scripts";
            const string PREFABS_FOLDER = "Prefabs";
            const string SCENES_FOLDER = "Scenes";
            const string DATA_FOLDER = "Data";
            const string PLUGINS_FOLDER = "Plugins";

            // Create the root folder which you can name
            VirtualFolder rootFolder = new(ASSETS_FOLDER, rootFolderName);
            VirtualFolder noVersion = new(ASSETS_FOLDER, NO_VERSION_CONTROL_FOLDER);

            rootFolder.AddSubFolder("Art").AddSubFolders(new string[] { "Animations", "Materials", "Models", "Shaders", "Sprites", "Textures", "Fonts" });

            // Add more top level folders.
            rootFolder.AddSubFolders(new string[] { AUDIO_FOLDER, SCRIPTS_FOLDER, PREFABS_FOLDER, SCENES_FOLDER, DATA_FOLDER, PLUGINS_FOLDER });

            rootFolder.AddSubFolder(SCENES_FOLDER).AddSubFolders(new string[] { "Game", "Prototype" });
            rootFolder.AddSubFolder("Scripts").AddSubFolders(new string[] { "Editor" });
            rootFolder.AddSubFolder("Scripts").AddSubFolder("Runtime").AddSubFolders(new string[] { "Implementations", "Systems", "Utility" });


            // By calling Realize() on the root folder, it will automatically create every folder we've added to it or its sub folders!
            rootFolder.Realize(createGitPlaceholder);
            noVersion.Realize(createGitPlaceholder);

            AssetDatabase.Refresh();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Enter name of the root folder: ");
            rootFolderName = EditorGUILayout.TextField("Root:", rootFolderName);
            createGitPlaceholder = EditorGUILayout.ToggleLeft("Placeholders", createGitPlaceholder);
            EditorGUILayout.HelpBox("When ticked, a .gitkeep file will be created in the folder to make Git keep it. This file will not be imported by Unity and will only be visible in the explorer.", MessageType.Info);


            if (GUILayout.Button("Generate"))
            {
                CreateFolders();
                Close();
            }
            EditorGUILayout.EndVertical();
        }
    }
}