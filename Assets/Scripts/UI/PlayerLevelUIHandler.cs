using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject levelUIFolderSerializable;

    private static Image _fillerImage;
    private static TextMeshProUGUI _textMeshProUGUI;

    private void Awake()
    {
        _fillerImage = levelUIFolderSerializable.transform.Find("Filler").GetComponent<Image>();
        _textMeshProUGUI = levelUIFolderSerializable.GetComponentInChildren<TextMeshProUGUI>();
    }

    public static void UpdateLevelUIInfo(int currentEssenceAmount, int levelUpEssenceAmount, int currentLevel)
    {
        _fillerImage.fillAmount = (float)currentEssenceAmount / levelUpEssenceAmount;
        _textMeshProUGUI.text = $"Level  {currentLevel}   {currentEssenceAmount}/{levelUpEssenceAmount}";
    }
}
