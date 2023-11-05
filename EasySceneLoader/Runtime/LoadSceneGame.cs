using UnityEngine;

namespace EasyPlugins.SceneLoad
{
	public class LoadSceneGame : MonoBehaviour
	{
		[SerializeField]
		private string _sceneName;

		[SerializeField]
		private int _indexScene;
		
		[SerializeField] 
		private bool _loadOnStart = false;

		private void Start()
		{
			if (_loadOnStart)
				LoadScene();
		}

		public void LoadScene() 
			=> SceneLoader.Instance.LoadLevel(_sceneName);
		
	}
}