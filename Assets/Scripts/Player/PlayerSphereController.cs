using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSphereController : MonoBehaviour
{
   [FormerlySerializedAs("prefab"), SerializeField]
   private GameObject prefabSerializable;

   private static GameObject _prefab;
   private static PoolObject _prefabPoolObject;
   private static Transform _sphereTransform;
   private static Vector3 _baseLocalPosition;

   private void Awake()
   {
      _sphereTransform = transform;
      _prefab = prefabSerializable;
      _prefabPoolObject = _prefab.GetComponent<PoolObject>();

      _baseLocalPosition = transform.localPosition;

      ObjectPooler.InitializeNewPool("Sphere trail", _prefab);
   }

   public static void Fly2Object(int damageAmount)
   {
      var selectedObjectPos = CursorHandler.SelectedObject.transform.position;
      var selectedObject = CursorHandler.SelectedObject;

      var instance = ObjectPooler.GetObjectFromPool(_prefabPoolObject.PoolName);
      instance.transform.localPosition = Vector3.zero;
      instance.transform.position = _sphereTransform.position;
      instance.transform.parent = _sphereTransform;

      var sequence = DOTween.Sequence();
      sequence.Append(_sphereTransform.DOMove(selectedObjectPos, 0.2f));
      sequence.AppendCallback(() =>
      {
         DealDamage();
      });

      void DealDamage()
      {
         // if object was destroyed after attack then we turn off selection
         if (selectedObject.ReceiveDamage(damageAmount)
             && CursorHandler.SelectedObject != null
             && Vector3Int.FloorToInt(CursorHandler.SelectedObject.transform.position) == Vector3Int.FloorToInt(selectedObjectPos))
            CursorHandler.TurnOffSelection();

         instance.transform.parent = null;
         _sphereTransform.localPosition = _baseLocalPosition;
      }
   }
}