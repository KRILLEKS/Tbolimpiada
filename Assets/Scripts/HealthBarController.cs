using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
   [SerializeField] private Image filler;

   private ObjectOnTile _objectOnTile;
   private float _maxHealthAmount;
   private float _currentHealthAmount;
   private GameObject _healthBarCanvas;

   private void Awake()
   {
      _healthBarCanvas = transform.GetChild(0).gameObject;

      TurnOffHealthBar();
   }

   public void InitializeHealthBar(ObjectOnTile objectOnTile, float maxHealthAmount)
   {
      _objectOnTile = objectOnTile;
      _maxHealthAmount = maxHealthAmount;
   }

   public void TurnOffHealthBar()
   {
      _healthBarCanvas.SetActive(false);
   }

   public void ResetHealthBar()
   {
      _currentHealthAmount = _maxHealthAmount;
      filler.fillAmount = 1;
   }

   // returns true if object was destroyed
   public bool ReceiveDamage(float damageAmount)
   {
      if (_healthBarCanvas.activeSelf == false)
         _healthBarCanvas.SetActive(true);

      _currentHealthAmount -= damageAmount;

      if (_currentHealthAmount > 0)
      {
         filler.fillAmount = _currentHealthAmount / _maxHealthAmount;
         return false;
      }
      else
      {
         _objectOnTile.Return2Pool();
         return true;
      }
   }
}