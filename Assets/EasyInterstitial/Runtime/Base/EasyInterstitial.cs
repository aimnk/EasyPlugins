using EasyPlugins.Utils;
using UnityEditor;
using UnityEngine;

namespace EasyPlugins.Interstitial
{
    using System;
    using System.Collections.Generic;

    public sealed class EasyInterstitial : MonoBehaviourSingleton<EasyInterstitial>
    {
        public static event Action onShowInterstitial = delegate { };

        public static event Action<string> onShowRewardAds = delegate { };

        [SerializeField] 
        private BridgeType _bridgeType = BridgeType.MockBridge;
        
        [SerializeField]
        private string _appodealKey;

        [SerializeField] 
        private float _timerPerInterstitial = 45;
        
        private IEasyInterstitial _easyInterstitial;

        private Dictionary<string, Action> _callbacks = new Dictionary<string, Action>();

        protected override void Init()
        {
            base.Init();
 
            DontDestroyOnLoad(gameObject);

            _easyInterstitial = SelectBridge(_bridgeType);
            
            _easyInterstitial.onShowInterstitial += OnShowInterstitial;
            _easyInterstitial.onShowRewardAds += OnShowRewardAds;
            _easyInterstitial.onErrorShowRewardAds += OnErrorRewardAds;
            _callbacks.Clear();
        }

        private IEasyInterstitial SelectBridge(BridgeType bridgeType)
        {
            IEasyInterstitial easyInterstitial = new MockBridge();

#if !UNITY_EDITOR
            switch (bridgeType)
            {
                case BridgeType.Appodeal:
                    easyInterstitial = new AppodealBridge(_appodealKey, _timerPerInterstitial);
                    break;
                case BridgeType.YandexSDK:
                    easyInterstitial = new YandexBridge();
                    break;
            }
#endif
            
            return easyInterstitial;
        }
        
        private void OnErrorRewardAds(string id)
        {
            if (_callbacks.TryGetValue(id, out var callback))
            {
                _callbacks.Remove(id);
            }
        }

        private void OnShowRewardAds(string id)
        {
            onShowRewardAds.Invoke(id);

            if (_callbacks.TryGetValue(id, out var callback))
            {
                callback.Invoke();
                _callbacks.Remove(id);
            }
        }

        private void OnShowInterstitial()
            => onShowInterstitial.Invoke();

        public void ShowInterstitial()
            => _easyInterstitial.ShowInterstitial();

        public void ShowRewardAd(string name)
            => _easyInterstitial.ShowRewardAd(name);

        public void ShowRewardAd(string name, Action callback)
        {
            if (_callbacks.ContainsKey(name))
            {
                _callbacks.Remove(name);
            }

            _callbacks.TryAdd(name, callback);
            _easyInterstitial.ShowRewardAd(name);
        }

        public bool IsAvailableRewardVideo()
            => _easyInterstitial.IsAvailableRewardVideo();

        private void OnDestroy()
        {
            _easyInterstitial.onShowInterstitial -= OnShowInterstitial;
            _easyInterstitial.onShowRewardAds -= OnShowRewardAds;
            _easyInterstitial.onErrorShowRewardAds -= OnErrorRewardAds;

            (_easyInterstitial as IDisposable)?.Dispose();
        }

        public void OnValidate()
        {
            SetDefines(_bridgeType.ToString());
        }
        
        private void SetDefines(string keyDefine)
        {
#if UNITY_EDITOR
            var availableTypes = Enum.GetValues(typeof(BridgeType));

            List<string> resultTypes = new List<string>();
            
            foreach (BridgeType typeBridge in availableTypes)
            {
                if (typeBridge.ToString() == keyDefine)
                    continue;
                
                resultTypes.Add(typeBridge.ToString());
            }
            
            var target = EditorUserBuildSettings.activeBuildTarget;
            var group = BuildPipeline.GetBuildTargetGroup(target);
        
            PlayerSettings.GetScriptingDefineSymbolsForGroup(group, out var defines);
        
            List<string> resultDefines = new List<string>();
        
            foreach (var define in defines)
            {
                if (resultTypes.Exists(x => x == define))
                    continue;

                resultDefines.Add(define);
            }

            resultDefines.Add(keyDefine);
        
            PlayerSettings.SetScriptingDefineSymbolsForGroup(group, resultDefines.ToArray()); 
#endif
        }
    }
    

    public enum BridgeType
    {
        MockBridge,
        YG_PLUGIN_YANDEX_GAME,
        APPODEAL_SDK
    }
}
