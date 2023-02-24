using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// must be attached to player
public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float movementSpeedSerializable;

   private static float _movementSpeed;

   private static Vector2Int _previousTilePos = Vector2Int.zero;
   private static bool _isAble2Move = true;
   private static Vector3 _rotationVector;

   private void Awake()
   {
      _movementSpeed = movementSpeedSerializable;
   }

   private void Update()
   {
      Rotate();
      Move();

      // TODO: improve movement so player can move diagonally next to the obstacle
      void Move()
      {
         var movementPosition = transform.position + (Vector3)(Input.MovementVector * (_movementSpeed * Time.deltaTime));
         
         // optimization
         if (_previousTilePos != Vector2Int.FloorToInt(movementPosition))
         {
            _previousTilePos = Vector2Int.FloorToInt(movementPosition);
            _isAble2Move = TilemapHandler.IsTileEmpty(_previousTilePos);
         }

         if (_isAble2Move)
            transform.position = movementPosition;
      }
      void Rotate()
      {
         if (Input.MovementVector.x != 0)
            _rotationVector = new Vector3(0,Input.MovementVector.x > 0 ? 0 : 180,0);
         
         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_rotationVector), 10f * Time.deltaTime);
      }
   }
}