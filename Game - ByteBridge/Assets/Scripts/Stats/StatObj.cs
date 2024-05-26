using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class StatObj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI StatName;
    [SerializeField] private TextMeshProUGUI StatLevel;
    private String statName;
    private PlayerManager _playerManager;
    public void SetValues(String name, float lvl)
    {
        StatName.text = name;
        statName = name;
        StatLevel.text = lvl.ToString();
    }

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
    }

    private void Update()
    {
        var modifiers = _playerManager.GetModifiers();
        foreach (var mod in modifiers)
        {
            if (mod.StatName == statName)
            {
                StatLevel.text = mod.StatValue.ToString();
            }
        }
    }
}