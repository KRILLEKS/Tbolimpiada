using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// must be attached to player
public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float movementSpeedSerializable;

   private static float _movementSpeed;
   
   private void Awake()
   {
      _movementSpeed = movementSpeedSerializable;
   }

   private void Update()
   {
      var movementVector = Input.MovementVector * (_movementSpeed * Time.deltaTime);
      transform.position = transform.position + (Vector3)movementVector;
   }
}
