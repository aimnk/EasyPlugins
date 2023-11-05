namespace EasyPlugins.Localize
{
    using System.Xml;
    using UnityEngine;

    public class LocalizatorXML : ILocalizator
    {
        public LocalizatorXML(string langToken)
        {
            _currentLanguageToken = langToken;
            Init();
        }

        private XmlDocument _xmlDoc;

        private XmlNodeList _nodeList;

        private readonly string _currentLanguageToken;

        private readonly SystemLanguage _currentLanguage;

        private string _languageToken;

        private void Init()
        {
            _xmlDoc = new XmlDocument();
            _xmlDoc.LoadXml(Resources.Load<TextAsset>("loc").text);
            _nodeList = _xmlDoc.GetElementsByTagName("key");

            _languageToken = _currentLanguageToken ?? "ru";
        }

        public string GetTranslation(string key)
        {
            foreach (XmlNode node in _nodeList)
            {
                XmlAttribute attr = node.Attributes["id"];
                if (attr != null && attr.Value == key)
                {
                    XmlNode translationNode = node.SelectSingleNode(_languageToken);
                    if (translationNode != null)
                    {
                        return translationNode.InnerText;
                    }
                }
            }
            
            Debug.LogError("Не найден перевод с ключем " + key);
            return key;
        }
    }
}

