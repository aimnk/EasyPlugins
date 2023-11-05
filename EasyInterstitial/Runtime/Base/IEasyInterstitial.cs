namespace EasyPlugins.Interstitial
{
    using System;

    public interface IEasyInterstitial
    {
        public event Action onShowInterstitial;

        public event Action<string> onShowRewardAds;

        public event Action<string> onErrorShowRewardAds;

        public void ShowInterstitial();

        public void ShowRewardAd(string name);

        public bool IsAvailableRewardVideo();
    }
}
