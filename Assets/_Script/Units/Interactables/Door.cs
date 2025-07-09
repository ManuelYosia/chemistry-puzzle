using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    [SerializeField] string scene;

    protected override void Interact()
    {
        // Show the interaction UI
        Debug.Log("Interacting with the door.");

        SceneManager.LoadScene(scene);
    }
}
