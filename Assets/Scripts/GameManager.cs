using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private TextMeshProUGUI lastDistanceText;
    [SerializeField] private TextMeshProUGUI bestDistanceText;
    [SerializeField] private GameObject _upgradeShopCanvas;
    
    [SerializeField] private GameObject _pauseMenuCanvas; // Pause menüsü paneli
    private bool isPaused = false; // Oyunun duraklatılıp duraklatılmadığını kontrol eder

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        // Oyuncunun mesafesini kaydet
        FindObjectOfType<DisplayDistanceText>().SaveDistance();

        // Kaydedilen mesafeleri al
        float lastDistance = PlayerPrefs.GetFloat("LastDistance", 0f);
        float bestDistance = PlayerPrefs.GetFloat("BestDistance", 0f);

        // UI Güncelle
        lastDistanceText.text = "Last Distance: " + lastDistance.ToString("F0") + "m";
        bestDistanceText.text = "Highest Distance: " + bestDistance.ToString("F0") + "m";

        // Game Over ekranını aç
        _gameOverCanvas.SetActive(true);

        // Oyunu durdur
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Oyunu devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenUpgradeShop()
    {
        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(false); // Game Over ekranını kapat
        }

        if (_upgradeShopCanvas != null)
        {
            _upgradeShopCanvas.SetActive(true); // Upgrade mağazasını aç
        }
    }

    public void CloseUpgradeShop()
    {
        if (_upgradeShopCanvas != null)
        {
            _upgradeShopCanvas.SetActive(false); // Upgrade mağazasını kapat
        }

        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(true); // Game Over ekranını geri aç
        }
    }

    public void CloseGameOverMenu()
    {
        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(false);
        }
    }
    
    public void OpenGameOverMenu()
    {
        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Zamanı normale döndür
        SceneManager.LoadScene("Menu Screen"); // "Menu Screen" sahnesini yükle
    }

    // ---- PAUSE MENÜ FONKSİYONLARI ----
    
    public void TogglePauseMenu()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (_pauseMenuCanvas != null)
        {
            _pauseMenuCanvas.SetActive(true);
        }
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (_pauseMenuCanvas != null)
        {
            _pauseMenuCanvas.SetActive(false);
        }
        Time.timeScale = 1f;
        isPaused = false;
    }
}
