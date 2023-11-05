using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace EasyPlugins.Interstitial
{
    public class MockBridge : IEasyInterstitial
    {
        public event Action onShowInterstitial = delegate { };

        public event Action<string> onShowRewardAds = delegate { };
        
        public event Action<string> onErrorShowRewardAds = delegate{  };

        public async void ShowInterstitial()
        {
            await UniTask.Delay(1000);
            onShowInterstitial.Invoke();
            Debug.Log("EasyInterstitial[MockBridge]: ShowInterstitial");
        }

        public async void ShowRewardAd(string name)
        {
            await UniTask.Delay(1000);
            onShowRewardAds.Invoke(name);
            Debug.Log("EasyInterstitial[MockBridge]: ShowRewardAd ID:" + name);
        }

        public bool IsAvailableRewardVideo()
            => true;
    }
}
