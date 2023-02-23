using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private float movementSpeedSerializable;
   
   private static GameObject _playerGO;
   private static float _zOffset;
   private static float _movementSpeed;

   private void Awake()
   {
      _playerGO = GameObject.FindWithTag("Player");
      _zOffset = transform.position.z;

      _movementSpeed = movementSpeedSerializable;
   }

   private void Update()
   {
      transform.position = Vector3.Lerp(transform.position, _playerGO.transform.position + new Vector3(0,0,_zOffset), _movementSpeed * Time.deltaTime);
   }
}
