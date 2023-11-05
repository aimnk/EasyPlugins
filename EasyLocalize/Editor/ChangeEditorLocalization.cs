using UnityEditor;
using UnityEngine;

namespace EasyPlugins.Localize
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
    }
}
