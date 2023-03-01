using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
   [SerializeField] private Camera mainCameraSerializable;
   [SerializeField] private GameObject mainCanvasGOSerializable;
   [SerializeField] private GameObject buildInfoGOSerializable;
   [SerializeField] private Color isAble2BuildColorSerializable;
   [SerializeField] private Color unable2BuildColorSerializable;

   private static Camera _mainCamera;
   private static GameObject _mainCanvasGO;
   private static GameObject _buildInfoGO;
   private static Color _isAble2BuildColor;
   private static Color _unable2BuildColor;
   private static PoolObject _currentTowerPoolObject;
   private static TowerSO _currentTowerSO;
   private static Vector2Int _offset;
   private static bool _isInBuildingMode;
   private static Vector2Int _previousObjectPos;
   private static bool _isAble2Build = false;

   private void Awake()
   {
      _mainCamera = mainCameraSerializable;
      _mainCanvasGO = mainCanvasGOSerializable;
      _buildInfoGO = buildInfoGOSerializable;
      _isAble2BuildColor = isAble2BuildColorSerializable;
      _unable2BuildColor = unable2BuildColorSerializable;

      foreach (var TowerSo in Resources.LoadAll<TowerSO>("Towers"))
         ObjectPooler.InitializeNewPool(TowerSo.towerType.ToString(), TowerSo.towerGO);

      Input.InvokeOnRightClick.AddListener(CancelBuildProcess);
      Input.InvokeOnLeftClick.AddListener(Build);
   }

   private void Update()
   {
      if (_isInBuildingMode)
         UpdatePos();
   }

   public static void StartBuildProcess(TowerSO towerSo, ItemCompound[] priceArray)
   {
      if (DayCycleController.IsItNight)
         return;

      _mainCanvasGO.SetActive(false);
      _buildInfoGO.SetActive(true);

      MainUIController.IsAble2SwitchMenuState = false;

      _isInBuildingMode = true;

      _offset = new Vector2Int(towerSo.size.x - 1, towerSo.size.y - 1); // cursor always will be in bot left corner 

      _previousObjectPos = new Vector2Int(-100, -100); // any value
      _currentTowerPoolObject = ObjectPooler.GetObjectFromPool(towerSo.towerType.ToString());
      _currentTowerSO = towerSo;
   }

   private static void UpdatePos()
   {
      if (_isInBuildingMode == false)
         return;

      var mousePos = _mainCamera.ScreenToWorldPoint(Input.MousePosition);
      var objectPos = Vector2Int.FloorToInt(new Vector2(mousePos.x, mousePos.y) + _offset);

      // optimization
      if (_previousObjectPos == objectPos)
         return;

      _currentTowerPoolObject.transform.position = new Vector3(objectPos.x, objectPos.y, 0);

      bool isEmpty = true;
      for (int x = 0; x <= _offset.x; x++)
         for (int y = 0; y <= _offset.y; y++)
         {
            var isTileEmpty = TilemapHandler.IsTileEmpty(objectPos - _offset + new Vector2Int(x, y));

            if (isTileEmpty == false)
               isEmpty = false;
         }

      if (isEmpty)
      {
         _currentTowerPoolObject.SpriteRenderer.color = _isAble2BuildColor;
         _isAble2Build = true;
      }
      else
      {
         _currentTowerPoolObject.SpriteRenderer.color = _unable2BuildColor;
         _isAble2Build = false;
      }
   }

   public static void Build()
   {
      if (_isAble2Build == false || _isInBuildingMode == false)
         return;

      _mainCanvasGO.SetActive(true);
      _buildInfoGO.SetActive(false);

      _isInBuildingMode = false;
      _isAble2Build = false;

      MainUIController.IsAble2SwitchMenuState = true;

      for (int i = 0; i < _currentTowerSO.price.Length; i++)
      {
         var price = _currentTowerSO.price[i];
         InventoryHandler.GetRidOfItem(price.ItemType, price.Amount);
      }

      for (int x = 0; x <= _offset.x; x++)
         for (int y = 0; y <= _offset.y; y++)
         {
            var mousePos = _mainCamera.ScreenToWorldPoint(Input.MousePosition);
            TilemapHandler.OccupyTile(TilemapHandler.GetTileOnPosition(Vector2Int.FloorToInt(new Vector2(mousePos.x, mousePos.y)) + new Vector2Int(x, y)));
         }
      
      InventoryUIHandler.UpdateInventoryInfo();
      BuidTabController.UpdateUIInfo();
   }

   public static void CancelBuildProcess()
   {
      if (_isInBuildingMode == false)
         return;

      MainUIController.IsAble2SwitchMenuState = true;

      _mainCanvasGO.SetActive(true);
      _buildInfoGO.SetActive(false);

      _isInBuildingMode = false;

      ObjectPooler.ReturnObject2Pool(_currentTowerSO.towerType.ToString(), _currentTowerPoolObject);
   }
}