using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {
    [SerializeField] TodoItemScriptable itemData;

    private void Start()
    {
        promptMessage = "Ambil Unsur " + itemData.elementName;
    }

    protected override void Interact()
    {
        itemData.isCompleted = true;
    }

}
