using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace EasyLocalize
{
    using System;
    using UnityEngine;

    public class Localizator : MonoBehaviour
    {
        public static event Action onSwitchLanguage = delegate {  };

        [SerializeField]
        private SystemLanguage _languageEditor;

        [SerializeField]
        private bool _useAutoFillNotFindLoc = false;
        
        public static ILocalizator Instance => _localizator ??= new LocalizatorXML(_dictionaryTokens[Application.systemLanguage]);

        private static Localizator _instance;

        private static ILocalizator _localizator;

        private static Dictionary<SystemLanguage, string> _dictionaryTokens = new()
        {
            { SystemLanguage.Russian, "ru"}, 
            { SystemLanguage.English, "en"},
            { SystemLanguage.Chinese, "zh"},
            { SystemLanguage.ChineseSimplified, "zh"},
            { SystemLanguage.ChineseTraditional, "zh"},
            { SystemLanguage.Spanish, "es"},
            { SystemLanguage.French, "fr"},
            { SystemLanguage.German, "de"},
            { SystemLanguage.Japanese, "ja"},
            { SystemLanguage.Korean, "ko"},
            { SystemLanguage.Italian, "it"},
            { SystemLanguage.Portuguese, "pt"},
            { SystemLanguage.Dutch, "nl"},
            { SystemLanguage.Romanian, "ro"},
            { SystemLanguage.Turkish, "tr"},
            { SystemLanguage.Indonesian, "id"},
            { SystemLanguage.Unknown, "en"},
        };
        
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

            if (Application.systemLanguage == SystemLanguage.Unknown)
            {
                CultureInfo myCulture = Thread.CurrentThread.CurrentCulture;
                _localizator = new LocalizatorXML(myCulture.TwoLetterISOLanguageName);
            }
            
#if UNITY_EDITOR
            _localizator = new LocalizatorXML(_dictionaryTokens[_languageEditor], _useAutoFillNotFindLoc);
#endif
        }
        
        public static void SwitchLanguage(string tokenLanguage)
        {
            _localizator = new LocalizatorXML(tokenLanguage);
            onSwitchLanguage.Invoke();

            if (Application.isEditor)
                Debug.Log("Switch language " + tokenLanguage);
        }
    }
}
