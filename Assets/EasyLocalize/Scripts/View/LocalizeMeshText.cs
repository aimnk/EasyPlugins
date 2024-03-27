using EasyLocalize;
using UnityEngine;

namespace Imported_Assets.EasyLocalize.View
{
    [RequireComponent(typeof(TextMesh))]
    public class LocalizeMeshText : LocalizeText
    {
        protected override string _localizeKey { get; set; }

        [SerializeField] private bool isUpper;
        
        private TextMesh _text;

        protected override void Awake()
        {
            base.Awake();
            _text = GetComponent<TextMesh>();
            _localizeKey = _text.text;
        }

        protected override void Localize() 
            => _text.text = isUpper ? GetLocalize().ToUpper() : GetLocalize();
    }
}
