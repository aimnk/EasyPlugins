using System;
using System.Collections;
using EasyPlugins.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EasyPlugins.SceneLoad
{
    public sealed class SceneLoader : MonoBehaviourSingleton<SceneLoader>
    {
        public static event Action<string> onStartSceneLoad = delegate{  };
        
        public static event Action<string> onCompleteSceneLoad = delegate{  };
        
        [SerializeField] private Slider _slider;

        [SerializeField] private Image _imageLoader;

        [SerializeField] private Text _textProgress;

        [SerializeField] private GameObject _loaderContent;
        
        [Range(0, 10)]
        [SerializeField] private float _timeDelayBeforeLoad = 0f;
        
        protected override void Init()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        public void Show(bool state)
        {
            _loaderContent.SetActive(state);
        }

        public void LoadLevel(string nameLevel)
        {
            StartCoroutine(LoadLevelAsync(nameLevel));
        }
        
        private IEnumerator LoadLevelAsync(string nameLevel)
        {
            onStartSceneLoad.Invoke(nameLevel);
            
            yield return new WaitForEndOfFrame();
			
            Instance.Show(true);
            yield return new WaitForSecondsRealtime(_timeDelayBeforeLoad);

            AsyncOperation async = SceneManager.LoadSceneAsync(nameLevel);

            if (Instance)
            {
                while (!async.isDone)
                {
                    UpdateProgress(async.progress);
                    yield return null;
                }

                yield return async;
            }
			
            onCompleteSceneLoad.Invoke(nameLevel);
            Show(false);
        }
        
        public void UpdateProgress(float asyncProgress)
        {
            if (_slider != null)
                _slider.value = asyncProgress;

            if (_imageLoader != null)
                _imageLoader.fillAmount = asyncProgress;

            if (_textProgress != null)
            {
                _textProgress.text = ((int)(asyncProgress * 100f)) + "%";
            }
        }
    }
}