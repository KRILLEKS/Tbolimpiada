using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHandler
{
   private static RectTransform _itemSlotsRectTransform;
   private static ItemSlot[] _itemSlots;

   private static Texture _inactiveSlotTexture;
   private static Texture _activeSlotTexture;

   private static int _currentItemSlotIndex = -1;
   // long names were made to simplify work with fields.
   // So each infoMenu field starts with "infoMenu" and each infoMenu value field starts from "selectedItem" 
   // selected item menu
   private static GameObject _infoMenuGO;
   private static RectTransform _infoMenuRectTransform;
   private static Vector2 _infoMenuAnchoredPos;
   private static CanvasGroup _infoMenuHoleCanvasGroup;
   private static CanvasGroup _infoMenuInfoCanvasGroup;
   private static RawImage _selectedItemIconRawImage;
   private static TextMeshProUGUI _selectedItemName;
   private static TextMeshProUGUI _selectedItemDescriptionTMP;
   private static GameObject _selectedItemEatButtonGO; // must be disabled if object isn't consumable
   private static TextMeshProUGUI _selectedItemDestroyButtonTMP;
   private static float _currentAmount2DestroySliderValue;
   private static int _currentAmount2Destroy;
   // item values
   private static GameObject _selectedItemFoodFolderGO; // must be disabled if object isn't consumable
   private static TextMeshProUGUI _selectedItemFoodAmountTMP;
   private static GameObject _selectedItemHealthFolderGO; // must be disabled if object isn't consumable
   private static TextMeshProUGUI _selectedItemHealthAmountTMP;
   private static TextMeshProUGUI _selectedItemEssenceAmountTMP;

   private class ItemSlot
   {
      public ItemSlot(Transform itemSlotFolder)
      {
         ItemSlotRawImage = itemSlotFolder.GetComponent<RawImage>();

         ItemIconRawImage = itemSlotFolder.Find("Icon").GetComponent<RawImage>();
         ItemAmountGO = itemSlotFolder.Find("Amount").gameObject;
         ItemAmountTMP = ItemAmountGO.GetComponent<TextMeshProUGUI>();
      }

      public RawImage ItemSlotRawImage { private set; get; }
      public RawImage ItemIconRawImage { private set; get; }
      public GameObject ItemAmountGO { private set; get; }

      public Item Item
      {
         set
         {
            if (value != null)
               ItemIconRawImage.texture = value.Sprite.texture;
            
            _item = value;
         }
         get
         {
            return _item;
         }
      }

      private Item _item;

      public int ItemAmount
      {
         set
         {
            _itemAmount = value;
            ItemAmountTMP.text = value.ToString();
            
            if (value == 0)
            {
               ItemAmountGO.SetActive(false);
               ItemIconRawImage.gameObject.SetActive(false);
               ItemSlotRawImage.texture = _inactiveSlotTexture;
            }
         }
         get
         {
            return _itemAmount;
         }
      }

      private int _itemAmount;


      private TextMeshProUGUI ItemAmountTMP;
   }

   public static void InitializeController(Transform inventoryFolderTransform)
   {
      _inactiveSlotTexture = Resources.Load<Texture2D>("UI/Inventory/InactiveItemSlot");
      _activeSlotTexture = Resources.Load<Texture2D>("UI/Inventory/ActiveItemSlot");

      var itemSlotsFolder = inventoryFolderTransform.Find("ItemSlots");
      _itemSlotsRectTransform = itemSlotsFolder.GetComponent<RectTransform>();

      InitializeItemSlotsArray();
      InitializeSelectedItemMenu();

      void InitializeItemSlotsArray()
      {
         _itemSlots = new ItemSlot[itemSlotsFolder.childCount];
         for (int i = 0; i < itemSlotsFolder.childCount; i++)
         {
            _itemSlots[i] = new ItemSlot(itemSlotsFolder.GetChild(i));

            _itemSlots[i].ItemSlotRawImage.texture = _inactiveSlotTexture;

            var index = i;
            itemSlotsFolder.GetChild(i).GetComponent<Button>().onClick.AddListener(() => SetActiveMenu(index));
         }
      }

      void InitializeSelectedItemMenu()
      {
         var selectedItemMenuTransform = inventoryFolderTransform.Find("SelectedItemMenu");

         // set info menu fields
         _infoMenuGO = selectedItemMenuTransform.gameObject;
         _infoMenuRectTransform = _infoMenuGO.GetComponent<RectTransform>();
         _infoMenuAnchoredPos = _infoMenuRectTransform.anchoredPosition;
         _infoMenuHoleCanvasGroup = _infoMenuRectTransform.GetComponent<CanvasGroup>();
         _infoMenuInfoCanvasGroup = _infoMenuRectTransform.Find("CanvasGroup").GetComponent<CanvasGroup>();

         // set info menu values fields
         var selectedItemMenuInfoFolder = _infoMenuInfoCanvasGroup.transform;
         _selectedItemIconRawImage = selectedItemMenuInfoFolder.Find("Icon").GetComponent<RawImage>();
         _selectedItemName = selectedItemMenuInfoFolder.Find("ItemName").GetComponent<TextMeshProUGUI>();
         _selectedItemDescriptionTMP = selectedItemMenuInfoFolder.Find("Description").GetComponent<TextMeshProUGUI>();
         _selectedItemEatButtonGO = selectedItemMenuInfoFolder.Find("Buttons").Find("Eat").gameObject;
         _selectedItemDestroyButtonTMP = selectedItemMenuInfoFolder.Find("Buttons").Find("Destroy").Find("Text").GetComponent<TextMeshProUGUI>();

         // set listeners
         _selectedItemEatButtonGO.GetComponent<Button>().onClick.AddListener(EatButton);
         selectedItemMenuInfoFolder.Find("Amount2Destroy").GetComponent<Slider>().onValueChanged.AddListener(SetAmount2Destroy);
         selectedItemMenuInfoFolder.Find("Buttons").Find("Destroy").GetComponent<Button>().onClick.AddListener(Destroy);

         // item values
         var consumeBenefitsFolder = selectedItemMenuInfoFolder.Find("ConsumeBenefits");
         _selectedItemFoodFolderGO = consumeBenefitsFolder.Find("Food").gameObject;
         _selectedItemFoodAmountTMP = _selectedItemFoodFolderGO.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
         _selectedItemHealthFolderGO = consumeBenefitsFolder.Find("Health").gameObject;
         _selectedItemHealthAmountTMP = _selectedItemHealthFolderGO.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
         _selectedItemEssenceAmountTMP = consumeBenefitsFolder.Find("Essence").Find("Amount").GetComponent<TextMeshProUGUI>();
      }
   }

   private static void EatButton()
   {
      InventoryHandler.EatItem(_itemSlots[_currentItemSlotIndex].Item.ItemType);

      GetRidOfItem(1);
   }

   private static void Destroy()
   {
      InventoryHandler.DestroyItem(_itemSlots[_currentItemSlotIndex].Item.ItemType, _currentAmount2Destroy);

      GetRidOfItem(_currentAmount2Destroy);
   }

   private static void GetRidOfItem(int amount)
   {
      _itemSlots[_currentItemSlotIndex].ItemAmount -= amount;

      if (_itemSlots[_currentItemSlotIndex].ItemAmount == 0)
      {
         _itemSlots[_currentItemSlotIndex].Item = null;
         
         _itemSlotsRectTransform.DOAnchorPos(new Vector2(0, 0), UIConstants.InventorySelectedItemMenuAppearTime).SetUpdate(true).SetEase(Ease.OutQuart);

         _infoMenuRectTransform
            .DOAnchorPos(new Vector2(_infoMenuAnchoredPos.x - 200f, _infoMenuRectTransform.anchoredPosition.y), UIConstants.InventorySelectedItemMenuAppearTime)
            .SetUpdate(true)
            .SetEase(Ease.OutQuart);
         
         _infoMenuHoleCanvasGroup.DOFade(0, UIConstants.InventorySelectedItemMenuAppearTime / 1.5f).SetUpdate(true).OnComplete(() =>
         {
            _infoMenuGO.SetActive(false);
         });
      }
      else
         SetAmount2Destroy(_currentAmount2DestroySliderValue);
   }

   private static void SetAmount2Destroy(float sliderValue)
   {
      _currentAmount2DestroySliderValue = sliderValue;
      _currentAmount2Destroy = Mathf.RoundToInt(sliderValue * _itemSlots[_currentItemSlotIndex].ItemAmount);

      if (_currentAmount2Destroy == 0)
         _currentAmount2Destroy = 1;

      _selectedItemDestroyButtonTMP.text = $"Destroy x{_currentAmount2Destroy}";
   }

   public static void UpdateInventoryInfo()
   {
      // essentials
      if (_currentItemSlotIndex != -1)
      {
         _itemSlots[_currentItemSlotIndex].ItemSlotRawImage.texture = _inactiveSlotTexture;
         _currentItemSlotIndex = -1;
      }

      _itemSlotsRectTransform.anchoredPosition = Vector2.zero;
      _infoMenuGO.SetActive(false);

      // set items
      var index = 0;
      foreach (var item in InventoryHandler.GetInventoryDictionary())
      {
         if (item.Value == 0)
            continue;

         if (_itemSlots[index].ItemIconRawImage.gameObject.activeSelf == false)
         {
            _itemSlots[index].ItemIconRawImage.gameObject.SetActive(true);
            _itemSlots[index].ItemAmountGO.SetActive(true);
         }

         _itemSlots[index].Item = ItemsHandler.GetItem(item.Key);
         _itemSlots[index].ItemAmount = item.Value;

         index++;
      }

      // set empty slots
      for (int i = index; i < Constants.InventoryMaxItemsAmount; i++)
      {
         if (_itemSlots[i].ItemIconRawImage.gameObject.activeSelf == false)
            continue;

         _itemSlots[i].Item = null;

         _itemSlots[i].ItemIconRawImage.gameObject.SetActive(false);
         _itemSlots[i].ItemAmountGO.SetActive(false);
      }
   }

   public static void SetActiveMenu(int menuIndex)
   {
      if (_itemSlots[menuIndex].Item == null)
         return;

      if (_currentItemSlotIndex != -1)
         _itemSlots[_currentItemSlotIndex].ItemSlotRawImage.texture = _inactiveSlotTexture;

      _currentItemSlotIndex = menuIndex;
      _itemSlots[_currentItemSlotIndex].ItemSlotRawImage.texture = _activeSlotTexture;

      // fade in selected item menu
      if (_infoMenuGO.activeSelf == false)
      {
         _infoMenuGO.SetActive(true);
         SetItemInfo();

         _itemSlotsRectTransform.DOAnchorPos(new Vector2(-235, 0), UIConstants.InventorySelectedItemMenuAppearTime).SetUpdate(true).SetEase(Ease.OutQuart);

         _infoMenuRectTransform.anchoredPosition = new Vector2(_infoMenuAnchoredPos.x - 200f, _infoMenuRectTransform.anchoredPosition.y);
         _infoMenuRectTransform.DOAnchorPos(_infoMenuAnchoredPos, UIConstants.InventorySelectedItemMenuAppearTime).SetUpdate(true).SetEase(Ease.OutQuart);
         _infoMenuHoleCanvasGroup.alpha = 0;
         _infoMenuHoleCanvasGroup.DOFade(1, UIConstants.InventorySelectedItemMenuAppearTime / 1.5f).SetUpdate(true);
      }
      // change selected item
      else
      {
         _infoMenuInfoCanvasGroup.DOFade(0, UIConstants.InventorySelectedItemMenuItemChangeTime / 2)
                                 .SetUpdate(true)
                                 .OnComplete(() =>
                                 {
                                    SetItemInfo();
                                    _infoMenuInfoCanvasGroup.DOFade(1, UIConstants.InventorySelectedItemMenuItemChangeTime / 2).SetUpdate(true);
                                 });
      }

      SetAmount2Destroy(_currentAmount2DestroySliderValue);

      void SetItemInfo()
      {
         var item = _itemSlots[menuIndex].Item;

         _selectedItemName.text = item.ItemGO.name;
         _selectedItemIconRawImage.texture = item.Sprite.texture;
         _selectedItemDescriptionTMP.text = item.Description;

         _selectedItemEssenceAmountTMP.text = item.EssenceGain.ToString();

         IsConsumable(item.IsConsumable);

         if (item.IsConsumable)
         {
            _selectedItemFoodAmountTMP.text = item.EnergyGain.ToString();
            _selectedItemHealthAmountTMP.text = item.HealthGain.ToString();
         }

         void IsConsumable(bool state)
         {
            _selectedItemEatButtonGO.SetActive(state);
            _selectedItemFoodFolderGO.SetActive(state);
            _selectedItemHealthFolderGO.SetActive(state);
         }
      }
   }
}