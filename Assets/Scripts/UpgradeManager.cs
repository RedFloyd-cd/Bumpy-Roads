using UnityEngine;
using TMPro; // TextMeshPro kullanımı için

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [Header("UI Referansları")]
    public TextMeshProUGUI fuelPriceText;
    public TextMeshProUGUI speedPriceText;
    public TextMeshProUGUI suspensionPriceText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Oyun başladığında fiyatları UI'ya yükle
        UpdateUpgradeUI();
    }

    public int GetUpgradeLevel(string upgradeName)
    {
        return PlayerPrefs.GetInt(upgradeName + "_Level", 0);
    }

    public int GetUpgradePrice(string upgradeName, int basePrice)
    {
        return PlayerPrefs.GetInt(upgradeName + "_Price", basePrice);
    }

    public void Upgrade(string upgradeName, int basePrice)
    {
        int currentMoney = ScoreManager.instance.GetMoney();
        int currentPrice = GetUpgradePrice(upgradeName, basePrice);

        if (currentMoney >= currentPrice)
        {
            ScoreManager.instance.SpendMoney(currentPrice);
            int newLevel = GetUpgradeLevel(upgradeName) + 1;
            PlayerPrefs.SetInt(upgradeName + "_Level", newLevel);

            int newPrice = Mathf.RoundToInt(currentPrice * 1.1f);
            PlayerPrefs.SetInt(upgradeName + "_Price", newPrice);

            PlayerPrefs.Save();
            Debug.Log($"{upgradeName} yükseltildi! Seviye: {newLevel}, Yeni fiyat: {newPrice}$");

            // UI Güncelle
            UpdateUpgradeUI();
        }
        else
        {
            Debug.Log("Yeterli paran yok!");
        }
    }

    private void UpdateUpgradeUI()
    {
        if (fuelPriceText != null)
            fuelPriceText.text = GetUpgradePrice("Fuel", 100) + "$";
        if (speedPriceText != null)
            speedPriceText.text = GetUpgradePrice("Speed", 150) + "$";
        if (suspensionPriceText != null)
            suspensionPriceText.text = GetUpgradePrice("Suspension", 200) + "$";
    }
}
