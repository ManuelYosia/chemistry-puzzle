using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class WinModal : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] GameObject todoItemPrefab;
    [SerializeField] TodoList todoList;

    TodoItem[] todoItems;


    // Start is called before the first frame update
    void Start()
    {
        ShowTodoList();
    }

    // Update is called once per frame
    void Awake()
    {
        
    }

    void ShowTodoList()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        todoItems = new TodoItem[todoList.todoItems.Length];

        for (int i = 0; i < todoList.todoItems.Length; i++)
        {
            GameObject todoItemObject = Instantiate(todoItemPrefab, container.transform);
            todoItemObject.transform.localPosition = Vector3.zero; // Reset position to avoid stacking issues

            todoItems[i] = todoItemObject.GetComponent<TodoItem>();
            if (todoItems[i] != null)
            {
                todoItems[i].SetTodoItem(todoList.todoItemsScriptables[i]);

            }
            else
            {
                Debug.LogWarning("TodoItem component not found on instantiated prefab.");
            }
        }
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
        GameManager.Instance.level++; // Increment level when player wins
        GameManager.Instance.SetGameState("Playing");
    }
}
