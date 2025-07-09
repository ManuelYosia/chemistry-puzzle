using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Text instructionText;
    [SerializeField] Text promptText;
    [SerializeField] GameObject todoList;
    [SerializeField] GameObject winModal;
    [SerializeField] GameObject loseModal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Lobby"))
        {
            winModal.SetActive(false);
            loseModal.SetActive(false);
            todoList.SetActive(false);

            if (PlayerPrefs.GetString("GameState") == "Win" || PlayerPrefs.GetString("GameState") == "GameOver")
            {
                GameManager.Instance.SetGameState("Playing");
            }
        } else
        {
            todoList.SetActive(true);
        }

        if (PlayerPrefs.GetString("GameState") == "Win" && !SceneManager.GetActiveScene().name.Equals("Lobby"))
        {
            winModal.SetActive(true);
        }
        else
        {
            winModal.SetActive(false);
         
        }

        if (PlayerPrefs.GetString("GameState") == "GameOver" && !SceneManager.GetActiveScene().name.Equals("Lobby"))
        {
            loseModal.SetActive(true);
        }
        else
        {
            loseModal.SetActive(false);

        }
    }

    public void ShowInstructionText()
    {
        instructionText.gameObject.SetActive(true);
    }

    public void HideInstructionText()
    {
        instructionText.gameObject.SetActive(false);
    }

    public void ShowPromptText(string interactableObjectName)
    {
        promptText.text = interactableObjectName;
        promptText.gameObject.SetActive(true);
    }

    public void HidePromptText()
    {
        promptText.gameObject.SetActive(false);
    }

    void ShowTodoList()
    {
        todoList.SetActive(true);
    }

    void HideTodoList()
    {
        todoList.SetActive(false);
    }
}
