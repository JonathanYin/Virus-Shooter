using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using TMPro;

public class PlayfabLogin : MonoBehaviour
{
    [SerializeField] private string username;
    [SerializeField] private string password;
    [SerializeField] private string email;

    public TMP_InputField inputID;

    #region Unity Methods
    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "374E0";
        }
    }
    #endregion

    #region Private Methods
    private bool IsValidUsername()
    {
        bool isValid = false;
        if (username.Length >= 3 && username.Length <= 24)
        {
            isValid = true;
        }
        return isValid;
    }
    private bool IsValidPassword()
    {
        bool isValid = false;
        if (password.Length >= 6 && password.Length <= 24)
        {
            isValid = true;
        }
        return isValid;
    }
    private void LoginWithCustomId()
    {
        Debug.Log($"Login to Playfab as {username}");
        var request = new LoginWithCustomIDRequest { CustomId = username, CreateAccount = true };

        var emailAddress = email;
        Debug.Log("email is " + emailAddress);
        // AddOrUpdateContactEmail(emailAddress);

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIdSuccess, OnFailure);
        // AddContactEmailToPlayer();
        // Debug.Log("added email");
    }

    private void UpdateDisplayName(string displayname){
        Debug.Log($"Updating Playfab account's display name to: {displayname}");
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayname};
        // callback for when displayname update succeeds, or results in error
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameSuccess, OnFailure);
    }

    #endregion

    #region Public Methods
    public void SetUsername(string name)
    {
        username = name;
        PlayerPrefs.SetString("USERNAME", username);
    }

    public void SetPassword(string pw)
    {
        password = pw;
        PlayerPrefs.SetString("PASSWORD", password);
    }

    // public void SetEmail(string em)
    // {
    //     email = em;
    //     PlayerPrefs.SetString("EMAIL", email);
    //     var request = new AddOrUpdateContactEmail{ }
    //     PlayFabClientAPI.AddOrUpdateContactEmail(request, email, )
    // }

    public void SetEmail(string address)
    {
        email = address;
        // AddContactEmailToPlayer();
    }

    public void SaveEmail() { 
        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string,string> { {"email",inputID.text.ToString()}}
        };
        PlayFabClientAPI.UpdateUserData(request, OnIDUpdateSuccess, OnError);
        Debug.Log("added email");
    }

    public void OnError(PlayFabError error)
    {
        // messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    public void OnIDUpdateSuccess(UpdateUserDataResult result) {
       Debug.Log("Success ID");
    }
    public void AddContactEmailToPlayer()
    {
        var loginReq = new LoginWithCustomIDRequest
        {
            CustomId = "374E0", // replace with your own Custom ID
            CreateAccount = true // otherwise this will create an account with that ID
        };

        // var emailAddress = "jonathanyin66@gmail.com"; 
        var emailAddress = email;
        Debug.Log("email is " + emailAddress);

        var request = new LoginWithCustomIDRequest { CustomId = username, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIdSuccess =>
        {
            Debug.Log("Successfully logged in player with PlayFabId");
            AddOrUpdateContactEmail(emailAddress);//Removed the parameter -- "PlayFabId"
        }, FailureCallback);
    }


    public void AddOrUpdateContactEmail(string emailAddress)//Removed the parameter -- "PlayFabId"
    {
        var request = new AddOrUpdateContactEmailRequest
        {
            //[Remove it]PlayFabId = playFabId,
            EmailAddress = emailAddress
        };
        PlayFabClientAPI.AddOrUpdateContactEmail(request, result =>
        {
            Debug.Log("The player's account has been updated with a contact email");
        }, FailureCallback);
    }


    
    public void Login()
    {
        if (!IsValidUsername() || !IsValidPassword())
        {
            Debug.Log($"Invalid username or password");
            return;
        }

        LoginWithCustomId();
    }
    #endregion

    #region Playfab Callbacks
    private void OnLoginCustomIdSuccess(LoginResult result)
    {
        Debug.Log($"You have logged into Playfab using custom id {username}");
        UpdateDisplayName(username);
    }

    private void OnFailure(PlayFabError error)
    {
        Debug.Log($"There was an issue with your request {error.GenerateErrorReport()}");
    }

    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result){
        Debug.Log($"You have updated the display name of the Playfab account!");
        SceneController.LoadScene("Menu");
    }

    private void FailureCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
    #endregion
}
