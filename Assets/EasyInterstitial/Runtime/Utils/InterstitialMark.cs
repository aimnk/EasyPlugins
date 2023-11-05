using UnityEngine;

namespace EasyPlugins.Interstitial
{

    public class InterstitialMark : MonoBehaviour
    {
        [SerializeField] private bool _useOnEnable, _useOnStart;

        private void OnEnable()
        {
            if (_useOnEnable)
                EasyInterstitial.Instance.ShowInterstitial();
        }

        private void Start()
        {
            if (_useOnStart)
                EasyInterstitial.Instance.ShowInterstitial();
        }
        
        public void MarkInterstitial() 
            => EasyInterstitial.Instance.ShowInterstitial();
    }
}
