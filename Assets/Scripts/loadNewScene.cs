using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadNewScene : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            SceneManager.LoadSceneAsync("intro_02");
        }
    }
}
