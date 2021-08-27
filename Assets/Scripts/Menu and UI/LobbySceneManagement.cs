using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.Escape))
        {
        	SceneManager.LoadScene("Menu"); 
 

        }
    }
    
}
