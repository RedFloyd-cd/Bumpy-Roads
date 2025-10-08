using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Mobil butonları basılı tutma için

public class DriveCar : MonoBehaviour
{
   [SerializeField] private Rigidbody2D _frontTireRB;
   [SerializeField] private Rigidbody2D _backTireRB;
   [SerializeField] private Rigidbody2D _carRb;
   [SerializeField] private float _speed = 150f;
   [SerializeField] private float _rotationSpeed = 300f;
   [SerializeField] private float _brakeForce = 200f; // Fren gücü

   private float _moveInput;
   private bool _isBraking = false;

   // Mobil Kontroller için değişkenler
   public Button moveLeftButton;
   public Button moveRightButton;
   public Button brakeButton;

   private void Start()
   {
      // Yükseltmeleri uygula
      _speed += UpgradeManager.instance.GetUpgradeLevel("Speed") * 10f;

      // Butonlara basılı tutma olayları ekle
      AddButtonEvents(moveLeftButton, () => _moveInput = -1, () => _moveInput = 0);
      AddButtonEvents(moveRightButton, () => _moveInput = 1, () => _moveInput = 0);
      AddButtonEvents(brakeButton, () => _isBraking = true, () => _isBraking = false);
   }

   private void FixedUpdate()
   {
      if (!_isBraking)
      {
         _frontTireRB.WakeUp();
         _backTireRB.WakeUp();
         _carRb.WakeUp();

         _frontTireRB.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
         _backTireRB.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
         _carRb.AddTorque(_moveInput * _rotationSpeed * Time.fixedDeltaTime);
      }

      if (_isBraking)
      {
         _carRb.linearVelocity = Vector2.Lerp(_carRb.linearVelocity, Vector2.zero, _brakeForce * Time.fixedDeltaTime);
      }
   }

   private void AddButtonEvents(Button button, Action onPress, Action onRelease)
   {
      EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

      EventTrigger.Entry pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
      pointerDown.callback.AddListener((eventData) => { onPress(); });
      trigger.triggers.Add(pointerDown);

      EventTrigger.Entry pointerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
      pointerUp.callback.AddListener((eventData) => { onRelease(); });
      trigger.triggers.Add(pointerUp);
   }
}
