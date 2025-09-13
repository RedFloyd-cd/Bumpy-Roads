using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource moveAudioSource; // Hareket sesi
    [SerializeField] private AudioSource idleAudioSource; // Rölanti (idle) sesi

    private void Start()
    {
        // Başlangıçta sesleri durdur
        moveAudioSource.Stop();
        idleAudioSource.Stop();

        // Idle sesi başlat
        idleAudioSource.Play();
    }

    private void Update()
    {
        // İleri tuşlarına basıldığını kontrol et
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!moveAudioSource.isPlaying) // Eğer hareket sesi çalmıyorsa
            {
                idleAudioSource.Stop();   // Rölanti sesini durdur
                moveAudioSource.Play();  // Hareket sesini başlat
            }
        }
        else
        {
            if (!idleAudioSource.isPlaying) // Eğer rölanti sesi çalmıyorsa
            {
                moveAudioSource.Stop();   // Hareket sesini durdur
                idleAudioSource.Play();   // Rölanti sesini başlat
            }
        }
    }
}