using System.Collections;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int coinValue = 5;    // Paranın değeri (5, 10, 25, 50 olabilir)
    [SerializeField] private AudioClip pickupSound; // Coin toplama sesi
    private AudioSource audioSource;               // AudioSource referansı

    private void Start()
    {
        // AudioSource bileşenini al
        audioSource = GetComponent<AudioSource>();

        // Eğer AudioSource bileşeni yoksa hata mesajı yazdır
        if (audioSource == null)
        {
            Debug.LogError("AudioSource bileşeni bulunamadı! Lütfen bir AudioSource ekleyin.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Eğer oyuncu coin'e dokunursa
        {
            // Oyuncunun parasını artır
            ScoreManager.instance.AddMoney(coinValue); // Skora coinin değerini ekle

            // Ses efektini çal
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // Objeyi yok et
            Destroy(gameObject);
        }
    }
}