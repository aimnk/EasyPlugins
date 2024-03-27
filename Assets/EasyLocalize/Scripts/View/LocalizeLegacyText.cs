using UnityEngine;
using UnityEngine.UI;

namespace EasyPlugins.Localize
{
    [RequireComponent(typeof(Text))]
    public class LocalizeLegacyText : LocalizeText
    {
        protected override string _localizeKey { get; set; }

        [SerializeField] private bool isUpper;
        

        private Text _text;

        protected override void Awake()
        {
            base.Awake();
            _text = GetComponent<Text>();
            _localizeKey = _text.text;
        }

        protected override void Localize() 
            => _text.text = isUpper ? GetLocalize().ToUpper() : GetLocalize();
    }
}
