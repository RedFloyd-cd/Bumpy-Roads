using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private AppOpenAd appOpenAd;
    private InterstitialAd interstitialAd;

    // 🎯 **Test ID'ler (Gerçek reklam için değiştir)**
    private string rewardedAdUnitId = "ca-app-pub-4513040819044297/3043778966"; // Test Rewarded Ad ID
    private string appOpenAdUnitId = "ca-app-pub-4513040819044297/2724310229";  // Test App Open Ad ID
    private string interstitialAdUnitId = "ca-app-pub-4513040819044297/9354131909"; // Test Interstitial Ad ID

    void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            LoadRewardedAd();
            LoadAppOpenAd();
            LoadInterstitialAd();
        });
    }

    // 📌 **Ödüllü Reklam Yükle**
    public void LoadRewardedAd()
    {
        AdRequest adRequest = new AdRequest();
        
        RewardedAd.Load(rewardedAdUnitId, adRequest, (ad, error) =>
        {
            if (error != null)
            {
                Debug.LogError("Ödüllü reklam yüklenirken hata oluştu: " + error.GetMessage());
                return;
            }
            
            rewardedAd = ad;
            Debug.Log("Ödüllü reklam başarıyla yüklendi.");
        });
    }

    // 📌 **Ödüllü Reklam Göster**
    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show(reward =>
            {
                Debug.Log("Kullanıcı ödül kazandı.");
            });
        }
        else
        {
            Debug.Log("Ödüllü reklam henüz yüklenmedi.");
        }
    }

    // 📌 **Geçiş Reklamı Yükle**
    public void LoadInterstitialAd()
    {
        AdRequest adRequest = new AdRequest();
        
        InterstitialAd.Load(interstitialAdUnitId, adRequest, (ad, error) =>
        {
            if (error != null)
            {
                Debug.LogError("Geçiş reklamı yüklenirken hata oluştu: " + error.GetMessage());
                return;
            }
            
            interstitialAd = ad;
            Debug.Log("Geçiş reklamı başarıyla yüklendi.");
        });
    }

    // 📌 **Geçiş Reklamı Göster**
    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Geçiş reklamı henüz yüklenmedi.");
        }
    }

    // 📌 **App Open Reklam Yükle**
    public void LoadAppOpenAd()
    {
        AdRequest adRequest = new AdRequest();
        
        AppOpenAd.Load(appOpenAdUnitId, adRequest, (ad, error) =>
        {
            if (error != null)
            {
                Debug.LogError("App Open reklam yüklenirken hata oluştu: " + error.GetMessage());
                return;
            }
            
            appOpenAd = ad;
            Debug.Log("App Open reklam başarıyla yüklendi.");
        });
    }

    // 📌 **App Open Reklam Göster**
    public void ShowAppOpenAd()
    {
        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            appOpenAd.Show();
        }
        else
        {
            Debug.Log("App Open reklam henüz yüklenmedi.");
        }
    }
}
