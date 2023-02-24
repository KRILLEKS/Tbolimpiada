using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input : MonoBehaviour
{
   public static Vector2 MovementVector;
   public static Vector3 MousePosition;
   
   private static InputMap _inputMap;

   private void Awake()
   {
      _inputMap = new InputMap();
   }

   private void OnEnable()
   {
      _inputMap.Enable();
   }

   private void Update()
   {
      MovementVector = _inputMap.Player.Move.ReadValue<Vector2>();
      MousePosition = UnityEngine.Input.mousePosition;
   }

   private void OnDisable()
   {
      _inputMap.Disable();
   }
}
