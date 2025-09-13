using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 0.5f;

    private List<AudioSource> _allAudioSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSoundSettings();

            // Sahne değiştiğinde çalışacak fonksiyonu ekle
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateAllAudioSources();
    }

    private void LoadSoundSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
        UpdateAllAudioSources();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
        UpdateAllAudioSources();
    }

    public void UpdateAllAudioSources()
    {
        _allAudioSources.Clear();
        _allAudioSources.AddRange(FindObjectsOfType<AudioSource>());

        foreach (AudioSource audioSource in _allAudioSources)
        {
            if (audioSource.CompareTag("Music"))
            {
                audioSource.volume = musicVolume;
            }
            else if (audioSource.CompareTag("SFX"))
            {
                audioSource.volume = sfxVolume;
            }
        }
    }

    // Yeni sahne yüklendiğinde ses kaynaklarını güncelle
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateAllAudioSources();
    }
}