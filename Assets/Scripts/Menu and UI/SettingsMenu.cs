using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject MainMenu;
    public GameObject Options;
    public GameObject CRISPR;
    public void SetVolume (float volume){
        // Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }

    void Update() {
         if (Input.GetKey(KeyCode.Escape) && Options.activeInHierarchy == true) {
            MainMenu.gameObject.SetActive(true);
            Options.gameObject.SetActive(false);
         }

         if (Input.GetKey(KeyCode.Escape) && CRISPR.activeInHierarchy == true) {
            MainMenu.gameObject.SetActive(true);
            CRISPR.gameObject.SetActive(false);
         }
    }
    }
