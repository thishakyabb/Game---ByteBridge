using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class ProceedButton : MonoBehaviour
{
    [SerializeField] private Button proceedButton;
    [SerializeField] private GameObject loadingSpinner;
    [SerializeField] private Button profileButton;
    [SerializeField] private TMP_Text prompt;
    private ApiManager apiManager;

    void Awake()
    {
        apiManager = GetComponent<ApiManager>();
        prompt.gameObject.SetActive(false);
        // Register a callback for the button click event
        proceedButton.onClick.AddListener(OnProceedButtonClick);
    }

    // Method to be called when the button is clicked
    void OnProceedButtonClick()
    {
        loadingSpinner.SetActive(true);
        StartCoroutine(apiManager.AuthenticateMockAPI(authenticated =>
        {
            StartCoroutine(apiManager.FetchProfile(user =>
             {
                 if (authenticated)
                 {
                     StartCoroutine(apiManager.GetQuestionnaireState(allowedforquestionnaire =>
                     {
                         proceedButton.GetComponentInChildren<TMP_Text>().text = "Go to game";
                         if (allowedforquestionnaire)
                         {
                             //need to do questionnaire

                             Application.OpenURL("http://localhost:3000/questionnaire/" + user.nic);
                             Debug.Log("redirected to questionnaire");
                             proceedButton.onClick.RemoveAllListeners();
                             proceedButton.onClick.AddListener(CheckQuestionnaireState);
                             prompt.gameObject.SetActive(true);
                         }
                         //Need to do questionnaire
                     }));
                     loadingSpinner.SetActive(false);
                     profileButton.gameObject.SetActive(true);
                     // If the API call is successful, transition to the next scene
                     // SceneManager.LoadScene(1);
                 }
             }));


        }));
    }
    void CheckQuestionnaireState()
    {
        StartCoroutine(apiManager.GetQuestionnaireState(result =>
        {
            if (result)
            {
                StartCoroutine(apiManager.FetchProfile(result =>
                {
                    if (string.IsNullOrEmpty(result.lastname) || string.IsNullOrEmpty(result.email))
                    {
                        prompt.text = "Please complete your profile before proceeding";
                    }
                }));
                Debug.Log("Load game");
            }
            //Need to do questionnaire
        }));

    }
}
