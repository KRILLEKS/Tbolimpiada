using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Input : MonoBehaviour
{
   public static readonly UnityEvent InvokeOnLeftClickRepeating = new (); // invokes if button is held
   public static readonly UnityEvent InvokeOnLeftClick = new (); // invokes if button is held
   public static readonly UnityEvent InvokeOnRightClick = new (); // invokes if button is held

   // TODO: handle differentAttackRanges
   public static float AttackRange; // for all towers and enemies

   public static Vector2 MovementVector;
   public static Vector3 MousePosition;

   private static InputMap _inputMap;
   private static bool _isLeftClickBeingHeld = false;

   private void Awake()
   {
      _inputMap = new InputMap();
   }

   private void OnEnable()
   {
      _inputMap.Enable();
   }

   private void Start()
   {
      _inputMap.Player.LeftClick.started += ctx => _isLeftClickBeingHeld = true;
      _inputMap.Player.LeftClick.canceled += ctx => _isLeftClickBeingHeld = false;

      _inputMap.Player.LeftClick.performed += ctx => InvokeOnLeftClick.Invoke();
      _inputMap.Player.RightClick.performed += ctx => InvokeOnRightClick.Invoke();
      
      _inputMap.Player.OpenMainMenu.performed += ctx => MainUIController.SwitchMainMenuState();
   }

   private void Update()
   {
      MousePosition = UnityEngine.Input.mousePosition;
      
      if (Time.timeScale == 0)
         return;
      
      MovementVector = _inputMap.Player.Move.ReadValue<Vector2>();
      
      if (_isLeftClickBeingHeld)
         InvokeOnLeftClickRepeating.Invoke();
   }

   private void OnDisable()
   {
      _inputMap.Disable();
   }
}
