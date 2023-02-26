using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
   [SerializeField] private bool isEnabled;

   private void Awake()
   {
      Debug.Log("Awake");
   }

   private void OnEnable()
   {
      Debug.Log("Enable");
   }
}
