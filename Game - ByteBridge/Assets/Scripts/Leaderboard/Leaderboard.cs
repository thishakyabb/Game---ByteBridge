using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Leaderboard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject leaderboardEntry;
    [FormerlySerializedAs("leaderboardScollContentHolder")] [SerializeField] private GameObject leaderboardScrollContentHolder;
    private ApiManager apiManager;
    void Start()
    {
        apiManager = ApiManager.Instance;
        StartCoroutine(apiManager.AuthenticateMockAPI(callback =>
            StartCoroutine(apiManager.GetLeaderboard(entries =>
                StartCoroutine(apiManager.GetLastMonthAverage(avrg=>
                      StartCoroutine(apiManager.GetUsageToday(tdy =>
                              {
                                  StartCoroutine(apiManager.FetchProfile(user =>
                                  {
                                    Debug.Log(entries);
                                    int i = 1;
                                    foreach (var entry in entries)
                                    {
                                        var o = Instantiate(leaderboardEntry, leaderboardScrollContentHolder.transform);
                                        var _leaderboardEntry = o.GetComponent<LeaderboardEntryMono>();
                                        _leaderboardEntry.killstext = entry.kills.ToString();
                                        _leaderboardEntry.numbertext = i.ToString()+".";
                                        _leaderboardEntry.wavestext = entry.waves.ToString();
                                        _leaderboardEntry.nametext = (user.nic == entry.nic) ? user.username : user.nic ;
                                        _leaderboardEntry.energysavedtext = (avrg - tdy).ToString("F2");
                                       i++;
                                    }
                                  }));
                            }
                         )
                      )
                   )
                )
            ))
        ));
    }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }
}
