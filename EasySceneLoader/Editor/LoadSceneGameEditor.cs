using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EasyPlugins.SceneLoad
{
	[CustomEditor(typeof(LoadSceneGame))]
	public class LoadSceneGameEditor : Editor
	{
		List<SceneAsset> m_SceneAssets = new List<SceneAsset>();
		
		SerializedProperty lookAtPoint;

		public List<string> scenes = new List<string>();
		
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			string[] dropOptions = new string[EditorBuildSettings.scenes.Length];

			for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
			{
				EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];

				string sceneName = Path.GetFileNameWithoutExtension(scene.path);

				dropOptions[i] = sceneName;
				scenes.Add(sceneName);
			}
			
			serializedObject.FindProperty("_indexScene").intValue = EditorGUILayout.Popup(serializedObject.FindProperty("_indexScene").intValue, dropOptions);
			serializedObject.FindProperty("_sceneName").stringValue = scenes[serializedObject.FindProperty("_indexScene").intValue ];
			
			EditorGUILayout.PropertyField(serializedObject.FindProperty("_loadOnStart"), new GUIContent("Load Scene On Start"), true);
			serializedObject.ApplyModifiedProperties();
		}
		
		public void SetEditorBuildSettingsScenes()
		{
			// Find valid Scene paths and make a list of EditorBuildSettingsScene
			List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
			foreach (var sceneAsset in m_SceneAssets)
			{
				string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
				if (!string.IsNullOrEmpty(scenePath))
					editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
			}

			// Set the Build Settings window Scene list
			EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
		}
	}
}