using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static void LoadScene(string name){
        SceneManager.LoadScene(name);
    }

    public void BackToLobby() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 5);
    }
}
