using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ErrorScene : MonoBehaviour
{
    [SerializeField] private string authorization = "Authorization";
    [SerializeField] private Button retryButton;

    void Awake()
    {
        // Register a callback for the button click event
        retryButton.onClick.AddListener(OnRetryButtonClick);
    }

    // Method to be called when the retry button is clicked
    void OnRetryButtonClick()
    {
        // Transition back to the scene with the API call button
        SceneManager.LoadScene(authorization);
    }
}
