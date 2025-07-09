using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TodoList : MonoBehaviour
{
    [SerializeField] GameObject todoPrefabs;
    public TodoItemScriptable[] todoItemsScriptables;

    public TodoItem[] todoItems { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CreateTodoItem();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTodoList();
    }

    public void CreateTodoItem()
    {
        if (todoItemsScriptables == null || todoItemsScriptables.Length == 0)
        {
            Debug.LogWarning("No Todo Items to create.");
            return;
        }

        todoItems = new TodoItem[todoItemsScriptables.Length];

        for (int i = 0; i < todoItemsScriptables.Length; i++)
        {
            GameObject todoItemObject = Instantiate(todoPrefabs, transform);

            todoItems[i] = todoItemObject.GetComponent<TodoItem>();
            if (todoItems[i] != null)
            {
                todoItems[i].SetTodoItem(todoItemsScriptables[i]);
                
            }
            else
            {
                Debug.LogWarning("TodoItem component not found on instantiated prefab.");
            }
        }
    }

    public void CheckTodoList()
    {
        bool allCompleted;
        int completedCount = 0;

        if (todoItems == null || todoItems.Length == 0)
        {
            Debug.LogWarning("Todo Items are not initialized.");
            return;
        }

        for (int i = 0; i < todoItemsScriptables.Length; i++)
        {
            if(todoItemsScriptables[i].isCompleted)
            {
                completedCount++;
            }
        }

        allCompleted = completedCount == todoItemsScriptables.Length;

        if (allCompleted && !SceneManager.GetActiveScene().name.Equals("Lobby"))
        {
            GameManager.Instance.SetGameState("Win");
        }
    }
}
