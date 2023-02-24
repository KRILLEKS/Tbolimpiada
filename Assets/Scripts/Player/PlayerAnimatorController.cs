using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
   private static readonly int IsMoving = Animator.StringToHash("IsMoving");
   private static readonly int Attack = Animator.StringToHash("Attack");
   
   private static Animator _animator;
   private static bool _playerMovementState = false;

   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   private void Update()
   {
      SetPlayerMovementState(Input.MovementVector != Vector2.zero);
      
      if (UnityEngine.Input.GetKey(KeyCode.Mouse0))
         SetAttackAnimation();
   }

   public static void SetPlayerMovementState(bool state)
   {
      if (state != _playerMovementState)
      {
         _animator.SetBool(IsMoving, state);
         _playerMovementState = state;
      }
   }

   public static void SetAttackAnimation()
   {
      if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Attack")
         return;
      
      _animator.SetTrigger(Attack);
   }
}
