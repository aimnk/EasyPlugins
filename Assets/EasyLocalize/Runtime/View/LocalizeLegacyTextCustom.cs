using System;
using UnityEngine;
using UnityEngine.UI;

namespace EasyLocalize.View
{
    public class LocalizeLegacyTextCustom : LocalizeText
    {
        protected override string _localizeKey { get; set; }

        [SerializeField] private bool isUpper;
        [SerializeField] private string _advancedText;
        
        private Text _text;

        protected override void Awake()
        {
            base.Awake();
            _text = GetComponent<Text>();
            _localizeKey = _text.text;
        }

        protected override void Localize() 
            => _text.text = isUpper ? String.Format(GetLocalize().ToUpper(), _advancedText): String.Format(GetLocalize(), _advancedText);
    }
}
