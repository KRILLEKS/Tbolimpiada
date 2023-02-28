using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
   [SerializeField] private bool isEnabled;

   [Button]
   public void InvokeMethod()
   {
      if (isEnabled == false)
         return;

   }
}
