using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçişleri için gerekli

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private string nextLevelName; // Bir sonraki seviyenin adı (Inspector'dan ayarlanabilir)
    [SerializeField] private GameObject finishEffect; // Tamamlanma efekti (isteğe bağlı)

    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;
            Debug.Log("Bölüm tamamlandı!");

            // Eğer bir efekt eklemek isterseniz
            if (finishEffect != null)
            {
                Instantiate(finishEffect, transform.position, Quaternion.identity);
            }

            // Biraz gecikme ile yeni bölümü yükle
            Invoke("LoadNextLevel", 2f);
        }
    }

    private void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogWarning("Sonraki bölüm ismi belirtilmedi! Lütfen Inspector'dan girin.");
        }
    }
}
