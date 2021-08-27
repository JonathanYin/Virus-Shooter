using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame (){
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
	}

	public void QuitGame (){
		Debug.Log("QUIT");
		Application.Quit();
		
	}

	public void Lobby (){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); 
	}

	public void Menu () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
	}

	public void Login () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
	}

	public void Instructions() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 6);
	}
}
