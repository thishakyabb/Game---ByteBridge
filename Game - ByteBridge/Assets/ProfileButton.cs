using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button profileButton;
    void Awake()
    {
        profileButton.gameObject.SetActive(false);
        profileButton.onClick.AddListener(OnProfileButtonClick);
    }

    // Update is called once per frame
    void OnProfileButtonClick()
    {
        SceneManager.LoadScene(3);
    }
}