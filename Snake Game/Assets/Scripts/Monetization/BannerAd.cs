using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms.

    void Start()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        if (InterstitialAd.Instance.AdsEnabled)
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            LoadBanner();
            ShowBannerAd();
        }
    }

    public void LoadBanner()
    {
        // Load the Ad Unit with banner content:
        if (InterstitialAd.Instance.AdsEnabled)
            Advertisement.Banner.Load(_adUnitId);
    }

    void ShowBannerAd()
    {
        // Show the loaded Banner Ad Unit:
        if (InterstitialAd.Instance.AdsEnabled)
            Advertisement.Banner.Show(_adUnitId);
    }
}
