using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Backtomenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button backtomenuButton;
    void Awake()
    {
        backtomenuButton.onClick.AddListener(OnBacktomenuButtonClick);
    }

    void OnBacktomenuButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
}
