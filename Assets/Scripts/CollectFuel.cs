using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFuel : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound; // Yakıt toplama sesi
    private AudioSource audioSource;               // AudioSource referansı

    private void Start()
    {
        // AudioSource bileşenini al
        audioSource = GetComponent<AudioSource>();

        // Eğer AudioSource bileşeni yoksa hata mesajı ver
        if (audioSource == null)
        {
            Debug.LogError("AudioSource bileşeni bulunamadı! Lütfen bir AudioSource ekleyin.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Yakıtı doldur
            FuelController.instance.FillFuell();

            // Pickup sesini çal
            if (audioSource != null && pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
            else
            {
                Debug.LogWarning("Pickup sesi çalınamadı! AudioSource veya PickupSound eksik.");
            }

            // Objeyi görünmez ve etkisiz yap
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            // Ses çaldıktan sonra objeyi yok et (Ses yoksa hemen yok et)
            Destroy(gameObject, pickupSound != null ? pickupSound.length : 0.1f);
        }
    }
}