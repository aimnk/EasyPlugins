namespace EasyLocalize
{
    using System.Xml;
    using UnityEngine;

    public class LocalizatorXML : ILocalizator
    {
        private bool _useAutoFill;
        
        public LocalizatorXML(string langToken, bool useAutoFill = false)
        {
            _useAutoFill = useAutoFill;
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

            _languageToken = _currentLanguageToken ?? "en";
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

                    XmlNode translationNodeDefault = node.SelectSingleNode("en");
                        
                    if (translationNodeDefault != null)
                    {
                        return translationNodeDefault.InnerText;
                    }
                }
            }

#if UNITY_EDITOR
            if (_xmlDoc.DocumentElement != null && _useAutoFill)
            {
                XmlElement personElem = _xmlDoc.CreateElement("key");
                
                XmlAttribute nameAttr = _xmlDoc.CreateAttribute("id");
                
                XmlElement enElement = _xmlDoc.CreateElement("en");
                
                XmlText nameText = _xmlDoc.CreateTextNode(key);
                
                XmlText enLoc = _xmlDoc.CreateTextNode(key);
                
                nameAttr.AppendChild(nameText);
                
                personElem.Attributes.Append(nameAttr);
                personElem.AppendChild(enElement);
                
                enElement.AppendChild(enLoc);
                
                _xmlDoc.DocumentElement.AppendChild(personElem);
                
                _xmlDoc.Save(Application.dataPath + "/Imported Assets/EasyLocalize/Resources/loc.xml");
                Debug.Log($"Ключ {key} не найден, добавляем в loc.xml");
            }
#endif
            return key;
        }
    }
}

