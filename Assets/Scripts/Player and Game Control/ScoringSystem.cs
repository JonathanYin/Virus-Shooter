using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoringSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int scoreValue; 
    public AudioSource collectSound;

    void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        scoreValue += 50;
        scoreText.GetComponent<Text>().text = "Score: " + scoreValue;
    }
}
