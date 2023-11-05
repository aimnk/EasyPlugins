
using System.Globalization;

namespace EasyPlugins.Interstitial
{
#if APPODEAL_SDK
    using System;
    using UnityEngine;
    using AppodealStack.Monetization.Api;
    using AppodealStack.Monetization.Common;

    public class AppodealBridge : IEasyInterstitial, IDisposable
    {
        public event Action onShowInterstitial = delegate { };

        public event Action<string> onShowRewardAds = delegate { };

        public event Action<string> onErrorShowRewardAds = delegate { };

        private float _timerPerInterstitial;
        
        private float _timeToShowAds;
        
        public AppodealBridge(string appKey, float timerPerInterstitial)
        {
            int adTypes = AppodealAdType.Interstitial | AppodealAdType.Banner | AppodealAdType.RewardedVideo | AppodealAdType.Mrec;
            _timerPerInterstitial = timerPerInterstitial;
            
            AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;
            AppodealCallbacks.RewardedVideo.OnFinished += OnShowRewardAds;
            AppodealCallbacks.RewardedVideo.OnFailedToLoad += OnFailedRewardLoad;
            AppodealCallbacks.RewardedVideo.OnShowFailed += OnFailedRewardShow;
            AppodealCallbacks.Interstitial.OnShown += OnShowInterstitial;
            Appodeal.Initialize(appKey, adTypes);
        }

        private void OnFailedRewardLoad(object sender, EventArgs e)
        {
            Debug.LogWarning("AdsManager[AppodealBridge]: RewardVideo load failed!");
        }

        private void OnFailedRewardShow(object sender, EventArgs e)
        {
            Debug.LogWarning("AdsManager[AppodealBridge]: RewardVideo show failed!");
        }

        private void OnShowInterstitial(object sender, EventArgs e)
        {
            onShowInterstitial.Invoke();
            Debug.Log("AdsManager[AppodealBridge]: Interstitial is shown");
            _timeToShowAds = Time.realtimeSinceStartup + _timerPerInterstitial;
        }

        private void OnShowRewardAds(object sender, RewardedVideoFinishedEventArgs e)
        {
            onShowRewardAds.Invoke(e?.Currency);
            Debug.Log("AdsManager[AppodealBridge]: Reward Shown with: "  + e?.Currency + " " +e?.Amount);
        }

        private void OnInitializationFinished(object sender, SdkInitializedEventArgs e)
        {
            Debug.Log("AdsManager[AppodealBridge]: is Init Complete!");
            Appodeal.Show(AppodealShowStyle.BannerBottom);
        }

        public void ShowInterstitial()
        {
            if (!IsAvailableInterstitialShow())
            {
                Debug.LogWarning("AdsManager[AppodealBridge]: time to show interstitial(sec): " + (_timeToShowAds - Time.realtimeSinceStartup).ToString(CultureInfo.InvariantCulture));
                return;
            }
            
            if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
            {
                Appodeal.Show(AppodealAdType.Interstitial);
            }
            else
            {
                Debug.LogWarning("AdsManager[AppodealBridge]: Interstitial not loaded!");
            }
        }

        public void ShowRewardAd(string name)
        {
            if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
            {
                Appodeal.Show(AppodealAdType.RewardedVideo, name);
            }
            else
            {
                Debug.LogWarning("AdsManager[AppodealBridge]: RewardVideo not loaded!");
            }
        }

        private bool IsAvailableInterstitialShow() 
            => _timeToShowAds <= Time.realtimeSinceStartup;

        public bool IsAvailableRewardVideo()
            => Appodeal.IsLoaded(AppodealAdType.RewardedVideo);

        public void Dispose()
        {
            AppodealCallbacks.Sdk.OnInitialized -= OnInitializationFinished;
            AppodealCallbacks.RewardedVideo.OnFinished -= OnShowRewardAds;
            AppodealCallbacks.Interstitial.OnShown -= OnShowInterstitial;
            AppodealCallbacks.RewardedVideo.OnFailedToLoad -= OnFailedRewardLoad;
            AppodealCallbacks.RewardedVideo.OnShowFailed -= OnFailedRewardShow;
        }
    }
#endif
}
