using UnityEngine;
using UnityEngine.UI;

public class MainMenuSoundController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // Slider referanslarını bul ve ayarları yükle
        FindSliders();
        InitializeSliders();
    }

    private void FindSliders()
    {
        // Eğer slider referansları eksikse sahnedeki slider'ları bul
        if (musicSlider == null || sfxSlider == null)
        {
            Slider[] sliders = FindObjectsOfType<Slider>();

            foreach (Slider slider in sliders)
            {
                if (slider.name == "MusicSlider")
                {
                    musicSlider = slider;
                }
                else if (slider.name == "SFXSlider")
                {
                    sfxSlider = slider;
                }
            }
        }
    }

    private void InitializeSliders()
    {
        if (SoundManager.Instance != null && musicSlider != null && sfxSlider != null)
        {
            // Önceki ayarları yükle
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

            // Slider değerleri değiştiğinde kaydet ve ses seviyesini güncelle
            musicSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);

            sfxSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }
    }

    private void OnMusicVolumeChanged(float value)
    {
        SoundManager.Instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    private void OnSFXVolumeChanged(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }
}