using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private float movementSpeedSerializable;
   [Range(1, 5)] [SerializeField] private float mouseOnCameraInfluence;

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
      var mousePositionNormalized =
         new Vector3(UnityEngine.Input.mousePosition.x / Screen.width - .5f, UnityEngine.Input.mousePosition.y / Screen.height - .5f, 0); // Range [-0.5f;0.5f]

      transform.position = Vector3.Lerp(transform.position,
                                        _playerGO.transform.position + new Vector3(0, 0, _zOffset) + mousePositionNormalized * mouseOnCameraInfluence,
                                        _movementSpeed * Time.deltaTime);
   }
}