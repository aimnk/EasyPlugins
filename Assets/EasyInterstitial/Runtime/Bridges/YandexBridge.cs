using System;

namespace EasyPlugins.Interstitial
{
#if YG_PLUGIN_YANDEX_GAME
    public class YandexBridge : IEasyInterstitial, IDisposable
    {
        public event Action onShowInterstitial = delegate { };

        public event Action<string> onShowRewardAds = delegate { };
        public event Action<string> onErrorShowRewardAds = delegate {  };

        public YandexBridge()
        {
            YandexGame.OpenFullAdEvent += OnOpenInterstitial;
            YandexGame.RewardVideoEvent += OnShowRewardAd;
        }

        private void OnShowRewardAd(int id)
            => onShowRewardAds.Invoke(id.ToString());

        private void OnOpenInterstitial()
            => onShowInterstitial.Invoke();

        public void ShowInterstitial()
        {
            YandexGame.FullscreenShow();
        }

        public void ShowRewardAd(string name)
        {
            if (int.TryParse(name, out var value))
            {
                YandexGame.RewVideoShow(value);
            }
            else
            {
                new ArgumentException("EasyInterstitial[YandexBridge]: Reward ID is null");
            }
        }
        
        public bool IsAvailableRewardVideo()
            => true;

        public void Dispose()
        {
            YandexGame.OpenFullAdEvent -= OnOpenInterstitial;
            YandexGame.RewardVideoEvent -= OnShowRewardAd;
        }
    }
#endif
}
