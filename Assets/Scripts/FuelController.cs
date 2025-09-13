using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelController : MonoBehaviour
{
   public static FuelController instance;

   [SerializeField] private Image _fuelImage;
   [SerializeField, Range(0.1f, 5f)] private float _fuelDrainSpeed = 1f;
   [SerializeField] private float _maxFuelAmount = 100f;
   [SerializeField] private Gradient _fuelGradient;

   private float _currentFuelAmount;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
   }

   private void Start()
   {
      // Yükseltmeleri uygula
      _maxFuelAmount += UpgradeManager.instance.GetUpgradeLevel("Fuel") * 20f; // Her seviye için +20 benzin
      _currentFuelAmount = _maxFuelAmount; // Başlangıçta dolu başlasın
      UpdateUI();

   }

   private void Update()
   {
      _currentFuelAmount -= Time.deltaTime * _fuelDrainSpeed;
      UpdateUI();

      if (_currentFuelAmount <= 0f)
      {
         GameManager.instance.GameOver();
      }
   }

   private void UpdateUI()
   {
      _fuelImage.fillAmount = (_currentFuelAmount / _maxFuelAmount);
      _fuelImage.color = _fuelGradient.Evaluate(_fuelImage.fillAmount);
   }

   public void FillFuell()
   {
      _currentFuelAmount = _maxFuelAmount;
      UpdateUI();
   }
}
