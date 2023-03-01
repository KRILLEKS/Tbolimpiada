using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeHandler : MonoBehaviour
{
   [SerializeField] private Transform tabFolder;
   [SerializeField] private Color unlockedTransitionColorSerializable;
   
   public static Color UnlockedTransitionColor;

   private static Texture _activeButtonTexture;
   private static Texture _inactiveButtonTexture;
   private static TextMeshProUGUI _availablePointsTMP;
   private static int _currentSkillTreeIndex = 0;
   private static RawImage[] _skillChoosingButtonRawImages;
   private static GameObject[] _skillTreesArray;

   private void Awake()
   {
      UnlockedTransitionColor = unlockedTransitionColorSerializable;
      
      _activeButtonTexture = Resources.Load<Texture2D>("UI/SkillTree/UnlockedSkill");
      _inactiveButtonTexture = Resources.Load<Texture2D>("UI/SkillTree/LockedSkill");

      _availablePointsTMP = tabFolder.Find("AvailablePoints").GetComponent<TextMeshProUGUI>();
      
      var skillTreesFolder = tabFolder.Find("SkillTrees");

      _skillTreesArray = new GameObject[skillTreesFolder.childCount];
      for (int i = 0; i < skillTreesFolder.childCount; i++)
         _skillTreesArray[i] = skillTreesFolder.GetChild(i).gameObject;
      
      var skillTreeChoosingButtonsFolder = tabFolder.Find("SkillTreeChoosing").Find("Buttons");
      for (int i = 0; i < _skillTreesArray.Length; i++)
      {
         int index = i;
         skillTreeChoosingButtonsFolder.GetChild(i).GetComponent<Button>().onClick.AddListener(() => SetSkillTree(index));
      }
      
      _skillChoosingButtonRawImages = new RawImage[_skillTreesArray.Length];
      for (int i = 0; i < _skillTreesArray.Length; i++)
         _skillChoosingButtonRawImages[i] = skillTreeChoosingButtonsFolder.GetChild(i).Find("Background").GetComponent<RawImage>();
   }

   private static void SetSkillTree(int index)
   {
      if (_currentSkillTreeIndex == index)
         return;
      
      _skillTreesArray[_currentSkillTreeIndex].SetActive(false);
      _skillTreesArray[index].SetActive(true);

      _skillChoosingButtonRawImages[_currentSkillTreeIndex].texture = _inactiveButtonTexture;
      _skillChoosingButtonRawImages[index].texture = _activeButtonTexture;

      _currentSkillTreeIndex = index;
   }

   public static void UpdateUIInfo()
   {
      _availablePointsTMP.text = "Available skill points: " + PlayerSkillsHandler.AvailableSkillPoints;
   }
}
