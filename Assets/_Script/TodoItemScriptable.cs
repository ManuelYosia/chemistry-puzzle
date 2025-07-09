using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTodoItem", menuName = "Todo Item")]
public class TodoItemScriptable : ScriptableObject
{
    public string todoText;
    public string elementName;
    public bool isCompleted;
    public int challengeId;

    Color completedColor = Color.green;

}
