using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpRequestManager : MonoBehaviour
{
    private static HttpRequestManager instance;
    public static HttpRequestManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("HttpRequestManager").AddComponent<HttpRequestManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private string apiUrl;
    private string jwtToken;

    public void Initialize(string apiUrl, string jwtToken)
    {
        this.apiUrl = apiUrl;
        this.jwtToken = jwtToken;
    }

    public IEnumerator LoadMainMenu()
    {
        // Perform additional background API calls or setup as needed
        // ...

        // Simulate loading process, e.g., fetching player data, initializing game assets, etc.
        yield return new WaitForSeconds(2f);

        // Now, the player is authenticated, and you can proceed to the main menu
        Debug.Log("Authentication successful. Loading main menu...");

        // Example: Make another API call using the centralized manager
        StartCoroutine(MakeApiCall("someEndpoint"));
    }

    public IEnumerator MakeApiCall(string endpoint)
    {
        string requestUrl = $"{apiUrl}/{endpoint}";

        using (UnityWebRequest request = UnityWebRequest.Get(requestUrl))
        {
            // Set JWT token in the request header
            request.SetRequestHeader("Authorization", $"Bearer {jwtToken}");

            // Send the request and handle the response
            yield return request.SendWebRequest();

            // Handle the response
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError($"API call failed: {request.error}");
            }
            else if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("API call successful. Response: " + request.downloadHandler.text);
                // Handle the response as needed
                // ...
            }
        }
    }
}

public class APIManager : MonoBehaviour
{
    private string apiKey = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGI5OjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhZg"; // Replace with your actual API key
    private string apiUrl = "http://20.15.114.131:8080/api";
    private string jwtToken;

    public Button apiCallButton; // Attach your button in the Unity editor

    void Start()
    {
        apiCallButton.onClick.AddListener(OnApiCallButtonClick);
    }

    void OnApiCallButtonClick()
    {
        StartCoroutine(AuthenticateAndMakeApiCall());
    }

    IEnumerator AuthenticateAndMakeApiCall()
    {
        string authenticationUrl = $"{apiUrl}/login/authenticate";

        // Create a UnityWebRequest for authentication
        using (UnityWebRequest authRequest = UnityWebRequest.Get(authenticationUrl))
        {
            // Set the API key in the request header
            authRequest.SetRequestHeader("Api-Key", apiKey);

            // Send the request and wait for the response
            yield return authRequest.SendWebRequest();

            // Check for errors
            if (authRequest.result == UnityWebRequest.Result.ConnectionError ||
                authRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError($"Authentication failed: {authRequest.error}");
            }
            else if (authRequest.result == UnityWebRequest.Result.Success)
            {
                // Authentication successful, extract JWT token from the response
                jwtToken = authRequest.downloadHandler.text;

                // Set up centralized HTTP request manager with JWT token
                HttpRequestManager.Instance.Initialize(apiUrl, jwtToken);

                // Make API call using the centralized manager
                StartCoroutine(HttpRequestManager.Instance.MakeApiCall("someEndpoint"));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Your update logic here
    }
}
