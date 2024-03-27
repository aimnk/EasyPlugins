using System;

namespace EasyLocalize
{
    using UnityEngine;
    
    public abstract class LocalizeText : MonoBehaviour
    {
        protected virtual string _localizeKey { get; set;}
        
        protected virtual void Awake() 
            => Localizator.onSwitchLanguage += Localize;
        
        protected virtual void OnDestroy() 
            => Localizator.onSwitchLanguage -= Localize;

        protected virtual void OnEnable() 
            => Localize();

        protected abstract void Localize();

        protected string GetLocalize() 
            => Localizator.Instance.GetTranslation(_localizeKey);
    }
}
