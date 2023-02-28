using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
   [Header("Essentials")]
   [SerializeField] private GameObject mainUICanvasSerializable;
   [SerializeField] private float blurFadeInTimeSerializable; // in seconds
   [Header("Color")]
   [SerializeField] private Color activeTabColorSerializable;
   [SerializeField] private Color inactiveTabColorSerializable;

   private static GameObject _mainUICanvas;
   private static float _blurFadeInTime;
   private static Color _activeTabColor;
   private static Color _inactiveTabColor;

   private static Dictionary<Tabs, GameObject> _tabs = new ();
   private static Dictionary<Tabs, Image> _selectionButtonImages = new ();
   private static Tabs _currentActiveTab;
   private static RawImage _blurRawImage;
   private static float _blurAlpha;
   private static Color _blurStartColor;

   private enum Tabs
   {
      PickaxeUpgrade,
      Inventory,
      Build,
      Settings
   }

   private void Awake()
   {
      _mainUICanvas = mainUICanvasSerializable;
      _blurFadeInTime = blurFadeInTimeSerializable;
      _activeTabColor = activeTabColorSerializable;
      _inactiveTabColor = inactiveTabColorSerializable;

      _blurRawImage = _mainUICanvas.transform.Find("Blur").GetComponent<RawImage>();
      var tabsArray = Enum.GetValues(typeof (Tabs)).Cast<Tabs>().ToArray(); // for SetButtons and FillTabsArray methods
      SetButtons();
      FillTabsDictionary();

      _blurAlpha = _blurRawImage.color.a;
      _blurStartColor = new Color(_blurRawImage.color.r, _blurRawImage.color.g, _blurRawImage.color.b, 0);

      _mainUICanvas.SetActive(false);
      
      SetTab(Tabs.Inventory);

      void SetButtons()
      {
         var tabSelectionTransform = _mainUICanvas.transform.Find("TabSelection");

         for (var index = 0; index < tabsArray.Length; index++)
         {
            var tabGO = tabSelectionTransform.Find(tabsArray[index].ToString())?.gameObject;

            if (tabGO == null)
               Debug.LogError($"tab selection button {tabsArray[index]} couldn't be found");
            else
            {
               var tab2SetIndex = index;
               tabGO.GetComponent<Button>().onClick.AddListener(() => SetTab(tabsArray[tab2SetIndex]));
               
               _selectionButtonImages.Add(tabsArray[index], tabGO.GetComponent<Image>());
            }

            _selectionButtonImages[tabsArray[index]].color = _inactiveTabColor;
         }
      }
      void FillTabsDictionary()
      {
         var tabsTransform = _mainUICanvas.transform.Find("Tabs");
         foreach (var tab in tabsArray)
         {
            var tabGO = tabsTransform.Find(tab.ToString())?.gameObject;

            if (tabGO == null)
               Debug.LogError($"tab {tab} couldn't be found");
            else
            {
               _tabs.Add(tab, tabGO);
               tabGO.SetActive(false);
            }
         }
      }
   }

   public static void SwitchMainMenuState()
   {
      // disable
      if (_mainUICanvas.activeSelf)
      {
         _blurRawImage.DOKill();
         _mainUICanvas.SetActive(false);
      }
      // enable
      else
      {
         _mainUICanvas.SetActive(true);
         _blurRawImage.color = _blurStartColor;
         _blurRawImage.DOFade(_blurAlpha, _blurFadeInTime).SetUpdate(true);
      }

      TimeController.SwitchTimeState();
   }

   private void SetTab(Tabs tab2Set)
   {
      if (_currentActiveTab == tab2Set)
         return;
      
      _selectionButtonImages[_currentActiveTab].color = _inactiveTabColor;
      _selectionButtonImages[tab2Set].color = _activeTabColor;
      
      _tabs[_currentActiveTab].SetActive(false);
      _tabs[tab2Set].SetActive(true);

      _currentActiveTab = tab2Set;
   }
}