using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;



public class UpdateContactEmail : MonoBehaviour
{
    [SerializeField] private string email;
    void Start()
    {
        // AddContactEmailToPlayer();
    }

    public void SetEmail(string address)
    {
        email = address;
        // AddContactEmailToPlayer();
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
        PlayFabClientAPI.LoginWithCustomID(loginReq, loginRes =>
        {
            Debug.Log("Successfully logged in player with PlayFabId: " + loginRes.PlayFabId);
            AddOrUpdateContactEmail(emailAddress);//Removed the parameter -- "PlayFabId"
        }, FailureCallback);
    }


    void AddOrUpdateContactEmail(string emailAddress)//Removed the parameter -- "PlayFabId"
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


    void FailureCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
}