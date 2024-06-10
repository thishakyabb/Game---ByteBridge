using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class ProceedButton : MonoBehaviour
{
    [SerializeField] private Button proceedButton;
    [SerializeField] private GameObject loadingSpinner;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button leaderBoardButton;
    [SerializeField] private TMP_Text prompt;
    
    private ApiManager apiManager;

    void Start()
    {
        apiManager = ApiManager.Instance;
        prompt.gameObject.SetActive(false);
        leaderBoardButton.gameObject.SetActive(false);
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
                     leaderBoardButton.gameObject.SetActive(true);
                     StartCoroutine(apiManager.GetQuestionnaireState(allowedforquestionnaire =>
                     {
                         proceedButton.GetComponentInChildren<TMP_Text>().text = "Go to game";
                         if (allowedforquestionnaire)
                         {
                             Debug.Log("need to do questionnaire again");
                             //need to do questionnaire

                             Application.OpenURL("http://localhost:3000/questionnaire/" + user.nic);
                             Debug.Log("redirected to questionnaire");
                             prompt.gameObject.SetActive(true);
                         } 
                         // not allowed to do questionnaire. meaning they've either already finished or in the process of doing it
                         proceedButton.onClick.RemoveAllListeners();
                         proceedButton.onClick.AddListener(CheckQuestionnaireState);
                         Debug.Log("added listener for onclick");
                     }));
                     loadingSpinner.SetActive(false);
                     profileButton.gameObject.SetActive(true);
                 }
             }));


        }));
    }
    void CheckQuestionnaireState()
    {
        StartCoroutine(apiManager.GetQuestionnaireState(allowedForQuestionnaire =>
        {
            Debug.Log("questionnaire state");
            Debug.Log(allowedForQuestionnaire);
            if (!allowedForQuestionnaire)
            {
                //not allowed for questionnaire. Meaning, questionnaire has been completed
                StartCoroutine(apiManager.FetchProfile(result =>
                      
                {
                    if (string.IsNullOrEmpty(result.lastname) || string.IsNullOrEmpty(result.email))
                    {
                        prompt.text = "Please complete your profile before proceeding";
                    }
                }));
                Debug.Log("loaded scene");
                SceneManager.LoadScene(4);
                Debug.Log("Load game");
            }
            //Need to do questionnaire
        }));

    }
}
