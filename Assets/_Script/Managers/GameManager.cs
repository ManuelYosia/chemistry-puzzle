using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    public int level; // Changed 'level' to public to match the accessibility of its setter.

    void Start()
    {
        if(PlayerPrefs.GetString("GameState") == "Playing")
        {
            SetGameState("Intro");
            level = 1;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("Game State set to: " + PlayerPrefs.GetString("GameState"));

    }
    public void SetGameState(string currentState)
    {
        PlayerPrefs.SetString("GameState", currentState);
        Debug.Log("Game State set to: " + PlayerPrefs.GetString("GameState"));
    }
}

