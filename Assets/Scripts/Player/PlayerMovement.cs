using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// must be attached to player
public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float movementSpeedSerializable;

   private static float _movementSpeed;

   private static SpriteRenderer[] _playerBodyParts;
   private static Vector2Int _previousTilePos = Vector2Int.zero;
   private static bool _isAble2Move = true;
   private static Vector3 _rotationVector;

   private void Awake()
   {
      _movementSpeed = movementSpeedSerializable;

      var texture = gameObject.transform.Find("Texture");
      _playerBodyParts = new SpriteRenderer[texture.childCount];
      Debug.Log(_playerBodyParts.Length);

      for (var index = 0; index < _playerBodyParts.Length; index++)
         _playerBodyParts[index] = texture.GetChild(index).GetComponent<SpriteRenderer>();
   }

   private void Update()
   {
      Move();
      Rotate();

      // TODO: improve movement so player can move diagonally next to the obstacle
      void Move()
      {
         var movementPosition = transform.position + (Vector3)(Input.MovementVector * (_movementSpeed * Time.deltaTime));

         // optimization
         if (_previousTilePos != Vector2Int.FloorToInt(movementPosition))
         {
            _previousTilePos = Vector2Int.FloorToInt(movementPosition);
            _isAble2Move = TilemapHandler.IsTileEmpty(_previousTilePos);
            
            SetSortingOrder();
         }

         if (_isAble2Move)
            transform.position = movementPosition;
      }
      void Rotate()
      {
         if (Input.MovementVector.x != 0)
            _rotationVector = new Vector3(0, Input.MovementVector.x > 0 ? 0 : 180, 0);

         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_rotationVector), 10f * Time.deltaTime);
      }
      void SetSortingOrder()
      {
         var sortingOrderBase = Mathf.FloorToInt(transform.position.y) * 3;
         _playerBodyParts[0].sortingOrder = 2 - sortingOrderBase; // body
         _playerBodyParts[1].sortingOrder = 3 - sortingOrderBase; // hand 
         _playerBodyParts[2].sortingOrder = 1 - sortingOrderBase; // leg
         _playerBodyParts[3].sortingOrder = 1 - sortingOrderBase; // leg
      }
   }
}