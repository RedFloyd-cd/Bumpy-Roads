using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public Sprite soundOnSprite;  // Ses açık ikonu
    public Sprite soundOffSprite; // Ses kapalı ikonu
    private Image buttonImage;
    private bool isSoundOn = true; // Başlangıçta ses açık

    private void Start()
    {
        buttonImage = GetComponent<Image>();

        // İlk açılışta kaydedilen sesi kontrol et
        isSoundOn = PlayerPrefs.GetInt("SoundState", 1) == 1;
        UpdateButtonSprite();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn; // Durumu değiştir

        // Ses kapalıysa sesi kapat, açıksa aç
        AudioListener.pause = !isSoundOn;

        // Yeni durumu kaydet
        PlayerPrefs.SetInt("SoundState", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();

        // Butonun sprite'ını güncelle
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        buttonImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
    }
}