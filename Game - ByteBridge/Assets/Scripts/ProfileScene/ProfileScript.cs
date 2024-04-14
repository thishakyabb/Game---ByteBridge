using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ProfileScript : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private ApiManager apiManager;
    public static ApiManager.User userprofile;
    void Start()
    {
        apiManager = GetComponent<ApiManager>();
        StartCoroutine(apiManager.FetchProfile(result =>
        {
            // If the API call is successful, populate the input fields
            PopulateInputFields(result);

            Debug.Log("Profile fetched");
        }));
    }
    void PopulateInputFields(ApiManager.User user)
    {
        // i
        var answerButton = uiDocument.rootVisualElement.Q<UnityEngine.UIElements.Button>("AnswerButton");
        answerButton.clicked += () => { Application.OpenURL("http://localhost:3000/answers/" + user.nic); };
        //setting userprofile so that other scripts can access it
        userprofile = user;

        //getting and setting the input field values
        var firstnameelement = uiDocument.rootVisualElement.Q<TextField>("firstnameinputfield");
        var lastnameelement = uiDocument.rootVisualElement.Q<TextField>("lastnameinputfield");
        var usernameelement = uiDocument.rootVisualElement.Q<TextField>("usernameinputfield");
        var nicelement = uiDocument.rootVisualElement.Q<TextField>("nicinputfield");
        var phoneelement = uiDocument.rootVisualElement.Q<TextField>("phoneinputfield");
        var emailElement = uiDocument.rootVisualElement.Q<TextField>("emailinputfield");


        lastnameelement.RegisterValueChangedCallback(evt => { userprofile.lastname = evt.newValue; });
        emailElement.RegisterValueChangedCallback(evt => { userprofile.email = evt.newValue; });


        firstnameelement.value = user.firstname;
        firstnameelement.SetEnabled(false);

        lastnameelement.value = user.lastname;

        usernameelement.value = user.username;
        usernameelement.SetEnabled(false);

        nicelement.value = user.nic;
        nicelement.SetEnabled(false);

        phoneelement.value = user.phoneNumber;
        phoneelement.SetEnabled(false);

        emailElement.value = user.email;

        // getting the answerButton

    }

}
