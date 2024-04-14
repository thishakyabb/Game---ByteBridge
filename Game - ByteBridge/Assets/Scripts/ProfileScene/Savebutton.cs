using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Savebutton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button saveButton;
    private ApiManager apiManager;
    private ProfileScript profileScript;
    void Awake()
    {
        apiManager = GetComponent<ApiManager>();
        profileScript = GetComponent<ProfileScript>();
        saveButton.onClick.AddListener(OnSaveButtonClick);
    }

    void OnSaveButtonClick()
    {
        Debug.Log("Save button clicked");
        StartCoroutine(apiManager.UpdateProfile(ProfileScript.userprofile, result =>
        {
        }));
    }

    // Update is called once per frame
}
