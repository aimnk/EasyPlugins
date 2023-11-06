using System.Collections.Generic;

namespace EasyPlugins.Localize
{
    using System;
    using UnityEngine;

    public class Localizator : MonoBehaviour
    {
        public static event Action onSwitchLanguage = delegate {  };
        
        public static ILocalizator Instance => _localizator ??= new LocalizatorXML(_dictionaryTokens[Application.systemLanguage]);

        private static Localizator _instance;

        private static ILocalizator _localizator;

        private static Dictionary<SystemLanguage, string> _dictionaryTokens = new()
        {
            { SystemLanguage.Russian, "ru"}, 
            { SystemLanguage.English, "en"}
        };

        private Action onEventData;
        
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }

#if YG_PLUGIN_YANDEX_GAME && !UNITY_EDITOR
            onEventData = () => SwitchLanguage(YG.YandexGame.EnvironmentData.language);
            YG.YandexGame.GetDataEvent += onEventData;
#endif
        }


#if YG_PLUGIN_YANDEX_GAME && !UNITY_EDITOR
        private void OnDestroy()
        {
            YG.YandexGame.GetDataEvent -= onEventData;
        }
#endif

        public static void SwitchLanguage(string tokenLanguage)
        {
            _localizator = new LocalizatorXML(tokenLanguage);
            onSwitchLanguage.Invoke();

            if (Application.isEditor)
                Debug.Log("Switch language " + tokenLanguage);
        }
    }
}
