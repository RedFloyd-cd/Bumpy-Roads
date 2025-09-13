using UnityEngine;
using TMPro; // TextMeshPro kütüphanesi

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;  // Singleton tasarımı
    private int score = 0;                // Oyuncunun toplam parası
    public TextMeshProUGUI scoreText;     // UI Skor metni

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahne değişince yok olmasını engeller
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        LoadMoney(); // Oyun başladığında parayı yükle
        UpdateScoreText();
    }

    public void AddMoney(int amount)
    {
        score += amount;
        SaveMoney(); // Değişiklikleri kaydet
        UpdateScoreText();
    }

    public void SpendMoney(int amount)
    {
        score -= amount;
        if (score < 0) score = 0; 
        SaveMoney(); // Değişiklikleri kaydet
        UpdateScoreText();
    }

    public int GetMoney()
    {
        return score;
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score + "$";
        }
        else
        {
            Debug.LogWarning("Score Text atanmamış! Unity'de bir UI TextMeshPro nesnesi atayın.");
        }
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt("PlayerMoney", score); // Skoru kaydet
        PlayerPrefs.Save(); // Değişiklikleri uygula
    }

    private void LoadMoney()
    {
        score = PlayerPrefs.GetInt("PlayerMoney", 0); // Kayıtlı skoru yükle (varsayılan 0)
    }
}