using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

public class ApiManager : MonoBehaviour
{
    // Replace with your actual API key and URL
    private string apiKey = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGI5OjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhZg";
    private string baseRemoteUrl = "http://20.15.114.131:8080/api/";
    private string baseLocalUrl = "http://localhost:8080/api/";
    public static string jwtToken;
    public static ApiManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public IEnumerator AuthenticateMockAPI(System.Action<bool> callback)
    {
        // Prepare the request body
        string jsonRequestBody = "{\"apiKey\":\"" + apiKey + "\"}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonRequestBody);

        // Set up the request headers
        Dictionary<string, string> headers = new Dictionary<string, string>
        {
            {"Content-Type", "application/json" }
        };

        // Make the API call to authenticate
        using (UnityWebRequest www = new UnityWebRequest(baseRemoteUrl + "login", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();

            foreach (var header in headers)
            {
                www.SetRequestHeader(header.Key, header.Value);
            }

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Parse the response to extract the JWT token
                string responseText = www.downloadHandler.text;
                jwtToken = ParseJwtToken(responseText);

                // Print the JWT token on the console
                Debug.Log("JWT Token: " + jwtToken);
                callback(true);
                // Call the API methods with the JWT token
            }
            else
            {
                Debug.LogError("Error making API call: " + www.error);
                callback(false);
                // If the API call is not successful, transition to the error scene
            }
        }
    }

    public IEnumerator GetQuestionnaireState(System.Action<bool> callback)
    {

        //making the second api call to get nic
        using (UnityWebRequest www = UnityWebRequest.Get(baseRemoteUrl + "user/profile/view"))
        {
            www.SetRequestHeader("Authorization", "Bearer " + jwtToken);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Parse the response to extract the user profile
                string responseText = www.downloadHandler.text;
                UserProfileResponse userProfile = JsonUtility.FromJson<UserProfileResponse>(responseText);

                // Use the 'nic' from the user profile to make the second API call
                string nic = userProfile.user.nic;

                //making api call to local springboot database to get authorization for questionnaire
                using (UnityWebRequest localWww = UnityWebRequest.Get(baseLocalUrl + "profiles/authorizedforquestionnaire/" + nic))
                {
                    yield return localWww.SendWebRequest();

                    if (localWww.result == UnityWebRequest.Result.Success)
                    {
                        // Handle the response here
                        string localResponseText = localWww.downloadHandler.text;
                        Debug.Log("Response: " + localResponseText);
                        callback(true);
                        yield return localResponseText;
                    }
                    else
                    {
                        Debug.LogError("Error making API call to local: " + localWww.error);
                        callback(false);
                        // If the API call is not successful, transition to the error scene
                        yield return false;
                    }
                }
            }
            else
            {
                Debug.LogError("Error making API call to remote 2nd: " + www.error);
                // If the API call is not successful, transition to the error scene
                callback(false);
                yield return false;
            }
        }

        // Hide spinner after API call completes
    }

    public IEnumerator FetchProfile(System.Action<User> callback)
    {
        // Prepare the request body
        Debug.Log("Fetching profile");
        Debug.Log(jwtToken);
        // Make the API call to authenticate
        using (UnityWebRequest www = UnityWebRequest.Get(baseRemoteUrl + "user/profile/view"))
        {
            www.SetRequestHeader("Authorization", "Bearer " + jwtToken);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Parse the response to extract the user profile
                string responseText = www.downloadHandler.text;
                UserProfileResponse userProfile = JsonUtility.FromJson<UserProfileResponse>(responseText);

                // Use the 'nic' from the user profile to make the second API call

                callback(userProfile.user);

            }
            else
            {
                Debug.LogError("Error making API call to remote: " + www.error);
                // If the API call is not successful, transition to the error scene

            }
        }
    }
    public IEnumerator UpdateProfile(User user, System.Action<User> callback)
    {
        // Convert the user object to a JSON string
        UpdateProfilePayload updateProfilePayload = new UpdateProfilePayload
        {
            firstname = user.firstname,
            lastname = user.lastname,
            nic = user.nic,
            phoneNumber = user.phoneNumber,
            email = user.email
        };

        // Convert the updateProfilePayload object to a JSON string
        string jsonRequestBody = JsonUtility.ToJson(updateProfilePayload);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonRequestBody); ;

        // Make the API call to update the user profile
        using (UnityWebRequest www = new UnityWebRequest(baseRemoteUrl + "user/profile/update", "PUT"))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer " + jwtToken);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseText = www.downloadHandler.text;
                UserProfileResponse userProfile = JsonUtility.FromJson<UserProfileResponse>(responseText);
                callback(userProfile.user);
                Debug.Log("Profile updated successfully");
            }
            else
            {
                Debug.LogError("Error updating profile: " + www.error);
            }
        }
    }

    public IEnumerator GetLastMonthAverage(Action<float> callback)
    {
        DateTime now = DateTime.Now;
        using (UnityWebRequest request = new UnityWebRequest(string.Format("{0}power-consumption/yearly/view?year={1}",baseRemoteUrl,now.Year)))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + jwtToken);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var monthlyUsage = JsonConvert.DeserializeObject<Dictionary<string,Dictionary<string,Dictionary<string,double>>>>(request.downloadHandler.text);
                string lastMonthName = now.AddMonths(-1).ToString("MMMM").ToUpper();
                var lastMonthUnits = (monthlyUsage["units"][lastMonthName]["units"]);
                callback?.Invoke((float)lastMonthUnits/30);

            }
            
        }
        
    }

    public IEnumerator GetUsageToday(Action<double> callback)
    {
        DateTime now = DateTime.Today;
        using (UnityWebRequest request = new UnityWebRequest($"{baseRemoteUrl}power-consumption/current-month/daily/view"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + jwtToken);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                RootPowerConsumptionDaily root =
                    JsonConvert.DeserializeObject<RootPowerConsumptionDaily>(request.downloadHandler.text);
                var today = now.Day;
                
                callback?.Invoke(root.dailyPowerConsumptionView.dailyUnits[today]);
            }
            
        }
    }

    public IEnumerator GetMarks(Action<int> callback,string nic)
    {
        using (UnityWebRequest request = new UnityWebRequest($"{baseLocalUrl}profiles/{nic}"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                int marks = JsonConvert.DeserializeObject<Data>(request.downloadHandler.text).marks;
                callback?.Invoke(marks);
            }
            
        }
    }
    private string ParseJwtToken(string responseText)
    {
        TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(responseText);
        return tokenResponse.token;
    }

    [System.Serializable]
    private class TokenResponse
    {
        public string token;
    }
    [System.Serializable]
    private class UserProfileResponse
    {
        public User user;
    }

    [System.Serializable]
    public class User
    {
        public string firstname;
        public string lastname;
        public string username;
        public string nic;
        public string phoneNumber;
        public string email;
    }

    
    public class DailyPowerConsumptionView
    {
        public int year { get; set; }
        public int month { get; set; }
        public Dictionary<int, double> dailyUnits { get; set; }
    }
    public class RootPowerConsumptionDaily
    {
        public DailyPowerConsumptionView dailyPowerConsumptionView { get; set; }
    }
    [System.Serializable]
    public class UpdateProfilePayload
    {
        public string firstname;
        public string lastname;
        public string nic;
        public string phoneNumber;
        public string email;
    }
    
    //Payload from local db
    public class Question
    {
        public int id { get; set; }
        public string question { get; set; }
        public List<string> answers { get; set; }
        public int correctAnswerIndex { get; set; }
        public int answeredIndex { get; set; }
        public string genericFeedback { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public int marks { get; set; }
        public string nic { get; set; }
        public List<Question> questions { get; set; }
    }
}
