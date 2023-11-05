using EasyPlugins.Utils;

namespace EasyPlugins.Interstitial
{
    public class ButtonInterstitialMark : AbstractButtonView
    {
        protected override void OnButtonClicked()
            => EasyInterstitial.Instance.ShowInterstitial();
    }
}
