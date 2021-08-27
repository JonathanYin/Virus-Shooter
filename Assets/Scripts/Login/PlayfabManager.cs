using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayfabManager : MonoBehaviour
{
    [Header("UI")]
    // public Text messageText;
    public TextMeshProUGUI messageText;
    public InputField inputID;

    public InputField email;

    public InputField emailInput;
    public InputField passwordInput;
    public GameObject rowPrefab;
    public Transform rowsParent;
    public string myID;

    public bool firstTime;

    public void RegisterButton()
    {
        if (passwordInput.text.Length < 6)
        {
            messageText.text = "Password too short!";
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registered and logged in!";
    }

    void OnIDUpdateSuccess(UpdateUserDataResult result) {
       Debug.Log("Success ID");
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in!";
        Debug.Log("Successful login/account creat!");
        // GetCharacters();
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null) {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        myID = result.PlayFabId.ToString();
    }
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "374E0"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Password reset email sent!";
    }
    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "Player High Scores",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard update");
    }

    public void SaveID() { 
        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string,string> { {"walletID",inputID.text.ToString()}}
        };
        PlayFabClientAPI.UpdateUserData(request, OnIDUpdateSuccess, OnError);
    }

    public void SavePriorExp() { 
        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string,string> { {"firstTime","yes"}}
        };
        PlayFabClientAPI.UpdateUserData(request, OnIDUpdateSuccess, OnError);
        

    }

    public void GetUserData(string myPlayFabeId) {
    PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
        PlayFabId = myPlayFabeId,
        Keys = null
    }, result => {
        Debug.Log("Got user data:");
        if (result.Data == null || !result.Data.ContainsKey("firstTime")){
            Debug.Log("No Data");
            firstTime = true;
        } 
        else {
            Debug.Log("First Time: "+result.Data["firstTime"].Value);
            if (!result.Data["firstTime"].Value.Equals("yes")) {
                Debug.Log(result.Data["firstTime"].Value.ToString() + "    sjalsjflasfjalkfjaklfjf");
                firstTime = true;
            } else {
                Debug.Log(result.Data["firstTime"].Value.ToString() + "    hjbkjkjlasfjalkfjaklfjf");

                firstTime = false;
            }
    }}, (error) => {
        Debug.Log("Got error retrieving user data:");
        Debug.Log(error.GenerateErrorReport());
    });}


    public void Goochi() {
        if (PlayFabClientAPI.IsClientLoggedIn() == true) {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 6);
                    Debug.Log("1");

        }        
    }

    void OnDataRecieved(GetUserDataResult result) {
        if (result.Data != null && result.Data.ContainsKey("firstTime")) {
            if (!result.Data["firstTime"].Value.Equals("yes")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 6);
                        Debug.Log("2");

            } else {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); 
                        Debug.Log("3");

            }
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 6);
                    Debug.Log("4");

        }
        SavePriorExp();
    }
    



    public void PlayGame (){
		if (firstTime == false) {
		    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
            Debug.Log("first" + firstTime.ToString());
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4); 
            Debug.Log("second" + firstTime.ToString());


        }
        Debug.Log("-poo: " + firstTime);
	}

    public void order() {
        Goochi();
    }

    public void GetLeaderboard(){
        var request = new GetLeaderboardRequest {
            StatisticName = "Player High Scores",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result){
        foreach (Transform item in rowsParent) {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard){
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}
