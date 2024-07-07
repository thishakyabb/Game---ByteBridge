using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LeaderboardButton: MonoBehaviour
{
   [FormerlySerializedAs("LeaderboardButton")] [SerializeField] private Button leaderboardButton;
   private void Start()
   {
      leaderboardButton.onClick.AddListener(GoHome);
   }

   public void GoHome()
   {
      SceneManager.LoadScene(5);
   }
}