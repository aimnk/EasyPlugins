using UnityEditor;
using UnityEngine;

namespace EasyLocalize
{
    public class ChangeLocalization : MonoBehaviour
    {
        [MenuItem("Localization/EN")]
        public static void ChangeEN()
        {
            Localizator.SwitchLanguage("en");
            Debug.Log("Смена языка на " + SystemLanguage.English);
        }

        [MenuItem("Localization/RU")]
        public static void ChangeRU()
        {
            Localizator.SwitchLanguage("ru");
            Debug.Log("Смена языка на " + SystemLanguage.Russian);
        }
        
        [MenuItem("Localization/de")]
        public static void ChangeDE()
        {
            Localizator.SwitchLanguage("de");
            Debug.Log("Смена языка на " + SystemLanguage.German);
        }
        
        [MenuItem("Localization/ZH")]
        public static void ChangeZH()
        {
            Localizator.SwitchLanguage("zh");
            Debug.Log("Смена языка на " + SystemLanguage.Chinese);
        }
        
        [MenuItem("Localization/es")]
        public static void ChangeES()
        {
            Localizator.SwitchLanguage("es");
            Debug.Log("Смена языка на " + SystemLanguage.Spanish);
        }
        
        [MenuItem("Localization/fr")]
        public static void ChangeFR()
        {
            Localizator.SwitchLanguage("fr");
            Debug.Log("Смена языка на " + SystemLanguage.French);
        }
        
        [MenuItem("Localization/JA")]
        public static void ChangeJA()
        {
            Localizator.SwitchLanguage("ja");
            Debug.Log("Смена языка на " + SystemLanguage.Japanese);
        }
        [MenuItem("Localization/JA")]
        public static void ChangeKO()
        {
            Localizator.SwitchLanguage("ko");
            Debug.Log("Смена языка на " + SystemLanguage.Korean);
        }
        
        [MenuItem("Localization/IT")]
        public static void ChangeIT()
        {
            Localizator.SwitchLanguage("it");
            Debug.Log("Смена языка на " + SystemLanguage.Italian);
        }
        
          
        [MenuItem("Localization/PT")]
        public static void ChangePT()
        {
            Localizator.SwitchLanguage("pt");
            Debug.Log("Смена языка на " + SystemLanguage.Portuguese);
        }
        [MenuItem("Localization/NL")]
        public static void ChangeNL()
        {
            Localizator.SwitchLanguage("nl");
            Debug.Log("Смена языка на " + SystemLanguage.Dutch);
        }
        [MenuItem("Localization/RO")]
        public static void ChangeRO()
        {
            Localizator.SwitchLanguage("ro");
            Debug.Log("Смена языка на " + SystemLanguage.Romanian);
        }
        [MenuItem("Localization/TR")]
        public static void ChangeTR()
        {
            Localizator.SwitchLanguage("tr");
            Debug.Log("Смена языка на " + SystemLanguage.Turkish);
        }
        [MenuItem("Localization/TR")]
        public static void ChangeID()
        {
            Localizator.SwitchLanguage("id");
            Debug.Log("Смена языка на " + SystemLanguage.Indonesian);
        }
        
        [MenuItem("Localization/HI")]
        public static void ChangeHI()
        {
            Localizator.SwitchLanguage("hi");
            Debug.Log("Смена языка на " + "Hindi");
        }
    }
}
