using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendTypeTab : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    public Button deleteButton;
    public Button addButton;
    public List<SendDataEntryId> entryId;

    public string NameText { get { return nameText.text; } set { nameText.text = value; } }


    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }
}

[Serializable]
public struct SendDataEntryId
{
    public string nameId;
    public string entryId;
    public SendDataEntryId(string nameId, string entryId)
    {
        this.nameId = nameId;
        this.entryId = entryId;
    }
}
