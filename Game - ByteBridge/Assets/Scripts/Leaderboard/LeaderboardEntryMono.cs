using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LeaderboardEntryMono: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gamertag;
    [SerializeField] private TextMeshProUGUI kills;
    [SerializeField] private TextMeshProUGUI number;
    [SerializeField] private TextMeshProUGUI waves;
    public string nametext;
    public string killstext;
    public string numbertext;
    public string wavestext;
    private void Start()
    {
        gamertag.text = nametext;
        kills.text = killstext;
        number.text = numbertext;
        waves.text = wavestext;
            
    }
}