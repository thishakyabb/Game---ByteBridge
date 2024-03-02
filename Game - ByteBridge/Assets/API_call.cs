using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    private string apiKey = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGI5OjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhZg"; // Replace with your actual API key
    private string apiUrl = "http://20.15.114.131:8080/api/login"; // Replace with your API base URL
    private string jwtToken;

    void Start()
    {
        StartCoroutine(AuthenticatePlayer());
    }

    IEnumerator AuthenticatePlayer()
    {
        string authenticationUrl = $"{apiUrl}/authenticate";

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
                StartCoroutine(LoadMainMenu());
            }
        }
    }

    IEnumerator LoadMainMenu()
    {
        // Perform additional background API calls or setup as needed
        // ...

        // Simulate loading process, e.g., fetching player data, initializing game assets, etc.
        yield return new WaitForSeconds(2f);

        // Now, the player is authenticated, and you can proceed to the main menu
        Debug.Log("Authentication successful. Loading main menu...");
    }

    // You can use this method to make other API calls with the JWT token
    IEnumerator MakeApiCall(string endpoint)
    {
        string requestUrl = $"{apiUrl}/{endpoint}";

        using (UnityWebRequest request = UnityWebRequest.Get(requestUrl))
        {
            // Set JWT token in the request header
            request.SetRequestHeader("Authorization", $"Bearer {jwtToken}");

            // Send the request and handle the response
            yield return request.SendWebRequest();

            // Handle the response as needed
            // ...
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Your update logic here
    }
}