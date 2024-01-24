using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class mainMenuManager : MonoBehaviour
{
    //add persistent storage here
    //load current scene from storage
    //private Scene currentScene

    public void startGame()
    {
        SceneManager.LoadSceneAsync("intro_01");//use currentScene
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
