using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuidTabController : MonoBehaviour
{
   [SerializeField] private Transform buildTabFolderSerializable;
   [SerializeField] private Color ifEnoughResourceTMPColorSerializable;
   [SerializeField] private Color ifNotEnoughResourceTMPColorSerializable;

   private static Dictionary<Constants.TowerTypes, TowerSO> _towerSos = new ();
   private static Dictionary<Constants.TowerTypes, Image> _towerButtonSelectionImages = new ();
   private static Sprite _activeTowerButtonSprite;
   private static Sprite _inactiveTowerButtonSprite;
   private static Transform _buildTabFolder;
   private static TextMeshProUGUI _nameTMP;
   private static ItemUICompound[] _itemUICompoundsArray;
   private static GameObject _notEnoughItemsTextGO;
   private static GameObject _buildButtonGO;
   private static Color _ifEnoughResourceTMPColor;
   private static Color _ifNotEnoughResourceTMPColor;
   private static Constants.TowerTypes _currentSelectedTower;
   private static bool _isAble2Build;

   private class ItemUICompound
   {
      public ItemUICompound(RawImage icon, TextMeshProUGUI amount)
      {
         Icon = icon;
         Amount = amount;
      }

      public RawImage Icon;
      public TextMeshProUGUI Amount;
   }

   private void Awake()
   {
      _buildTabFolder = buildTabFolderSerializable;
      _ifEnoughResourceTMPColor = ifEnoughResourceTMPColorSerializable;
      _ifNotEnoughResourceTMPColor = ifNotEnoughResourceTMPColorSerializable;

      var _activeTowerButtonTexture = Resources.Load<Texture2D>("UI/Building/ActiveButton");
      var _inactiveTowerButtonTexture = Resources.Load<Texture2D>("UI/Building/InactiveButton");

      _activeTowerButtonSprite = Sprite.Create(_activeTowerButtonTexture,
                                               new Rect(0, 0, _activeTowerButtonTexture.width, _activeTowerButtonTexture.height),
                                               new Vector2(.5f, .5f));
      _inactiveTowerButtonSprite = Sprite.Create(_inactiveTowerButtonTexture,
                                                 new Rect(0, 0, _inactiveTowerButtonTexture.width, _inactiveTowerButtonTexture.height),
                                                 new Vector2(.5f, .5f));

      _nameTMP = _buildTabFolder.Find("Name").GetComponent<TextMeshProUGUI>();

      _notEnoughItemsTextGO = _buildTabFolder.Find("NotEnoughItems").gameObject;
      _buildButtonGO = _buildTabFolder.Find("Build").gameObject;

      var buildPriceFolder = _buildTabFolder.Find("BuildPrice");
      _itemUICompoundsArray = new ItemUICompound[buildPriceFolder.childCount];
      for (int i = 0; i < _itemUICompoundsArray.Length; i++)
      {
         var price = buildPriceFolder.GetChild(i);
         _itemUICompoundsArray[i] = new ItemUICompound(price.Find("Icon").GetComponent<RawImage>(), price.Find("Text").GetComponent<TextMeshProUGUI>());
      }

      foreach (var towerSo in Resources.LoadAll<TowerSO>("Towers"))
         _towerSos.Add(towerSo.towerType, towerSo);

      var _towerSelectionButtonsTransform = _buildTabFolder.Find("Building selection");
      foreach (var tower in _towerSos)
         _towerButtonSelectionImages[tower.Key] = _towerSelectionButtonsTransform.Find(tower.Value.towerType.ToString()).GetComponent<Image>();

      foreach (var tower in _towerSos)
         _towerSelectionButtonsTransform.Find(tower.Value.towerType.ToString()).GetComponent<Button>().onClick.AddListener(() => SetTower(tower.Key));

      _buildTabFolder.Find("Build").GetComponent<Button>().onClick.AddListener(() =>BuildController.StartBuildProcess(_towerSos[_currentSelectedTower], _towerSos[_currentSelectedTower].price));

      SetTower(Constants.TowerTypes.Catapult, true);
   }

   public static void UpdateUIInfo()
   {
      SetTower(_currentSelectedTower, true);
   }

   private static void SetTower(Constants.TowerTypes towerType, bool repeatMethod = false)
   {
      if (repeatMethod == false && _currentSelectedTower == towerType)
         return;

      _isAble2Build = true;

      _towerButtonSelectionImages[_currentSelectedTower].sprite = _inactiveTowerButtonSprite;

      _currentSelectedTower = towerType;

      _towerButtonSelectionImages[_currentSelectedTower].sprite = _activeTowerButtonSprite;
      _nameTMP.name = _towerSos[_currentSelectedTower].name;

      // set resources
      var priceArray = _towerSos[_currentSelectedTower].price;
      for (int index = 0; index < _itemUICompoundsArray.Length; index++)
      {
         _itemUICompoundsArray[index].Amount.transform.parent.gameObject.SetActive(index < priceArray.Length);

         _itemUICompoundsArray[index].Icon.texture = ItemsHandler.GetItem(priceArray[index].ItemType).Sprite.texture;
         _itemUICompoundsArray[index].Amount.text = priceArray[index].Amount.ToString();
      }

      // check if can build
      for (int index = 0; index < _itemUICompoundsArray.Length; index++)
      {
         if (index >= priceArray.Length)
            return;

         int amountThatPlayerHas = InventoryHandler.GetItemAmount(priceArray[index].ItemType);
         if (amountThatPlayerHas < priceArray[index].Amount)
         {
            _itemUICompoundsArray[index].Amount.color = _ifNotEnoughResourceTMPColor;
            _isAble2Build = false;
         }
         else
         {
            _itemUICompoundsArray[index].Amount.color = _ifEnoughResourceTMPColor;
         }
      }

      _notEnoughItemsTextGO.SetActive(_isAble2Build == false);
      _buildButtonGO.SetActive(_isAble2Build);
   }
}