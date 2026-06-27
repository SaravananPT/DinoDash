using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    private readonly string interstitialAdUnitId =
#if UNITY_IOS
        "ca-app-pub-3940256099942544/4411468910"; // Test ID iOS
#else
        "ca-app-pub-3940256099942544/1033173712"; // Test ID Android
#endif

    private readonly string rewardedAdUnitId =
#if UNITY_IOS
        "ca-app-pub-3940256099942544/1712485313"; // Test ID iOS
#else
        "ca-app-pub-3940256099942544/5224354917"; // Test ID Android
#endif

    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        MobileAds.Initialize(_ =>
        {
            LoadInterstitial();
            LoadRewarded();
        });
    }

    private void LoadInterstitial()
    {
        InterstitialAd.Load(interstitialAdUnitId, new AdRequest(), (ad, error) =>
        {
            if (error != null) return;
            interstitialAd = ad;
        });
    }

    public void ShowInterstitial()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
            interstitialAd.OnAdFullScreenContentClosed += () => LoadInterstitial();
        }
    }

    private void LoadRewarded()
    {
        RewardedAd.Load(rewardedAdUnitId, new AdRequest(), (ad, error) =>
        {
            if (error != null) return;
            rewardedAd = ad;
        });
    }

    public void ShowRewarded(Action onSuccess)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show(_ =>
            {
                onSuccess?.Invoke();
                LoadRewarded();
            });
        }
    }
}
