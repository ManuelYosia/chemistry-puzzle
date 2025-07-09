using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TodoItem : MonoBehaviour
{
    Toggle toggle;
    TextMeshProUGUI text;
    TodoItemScriptable todoItem;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = todoItem.todoText;
        toggle.isOn = todoItem.isCompleted;
    }

    public void SetTodoItem(TodoItemScriptable todoItem)
    {
        this.todoItem = todoItem;
    }
}
