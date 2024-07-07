using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PopupStat: MonoBehaviour
{
   public string id;
   public TextMeshProUGUI TMP;
   public string text;
   public TMP_FontAsset fontAsset;
   public Color color;

    private void Start()
    {
        TMP.text = text;
        TMP.color = color;
        TMP.font = fontAsset;
    }
}