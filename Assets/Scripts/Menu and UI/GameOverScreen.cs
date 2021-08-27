using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public int score;
    //public Text text;
    public void Setup (int score) {
        gameObject.SetActive(true);
        //score = PlayerPrefs.GetInt("score");
        //pointsText.SetText(score.ToString());
        //pointsText.ForceMeshUpdate(true);
        //pointsText.text = score.ToString() + " POINTS";
    }

    void Update() {
        pointsText.SetText("Points: " + PlayerPrefs.GetInt("GameScore").ToString());
        pointsText.ForceMeshUpdate(true);
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4); 
        }
    }

    public void PlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2); 
    }

    public void MainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    
}
