using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskItem : MonoBehaviour
{
    bool status;
    string itemName;
    string dateTime;

    //  public Slider progress;
    // public InputField inputField;
    public GameObject nameInputField, dateInputField;

    public GameObject edit, delete;

    public TextMeshProUGUI dateText, itemNameText;

    public string DateTime { get => dateTime; set => dateTime = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public bool Status { get => status; set => status = value; }


    public void StatusToggle(bool value) {

        Status = value;

    }

    public void UpdateNameText(string text) {

        itemNameText.text = text;

    }
    public void UpdateDateText(string text)
    {

        dateText.text = text;

    }

    public void ToggleControls()
    {

        edit.SetActive(!edit.activeInHierarchy);
        delete.SetActive(!delete.activeInHierarchy);

    }

    public void ToggleEdit()
    {

        nameInputField.SetActive(!nameInputField.activeInHierarchy);
        dateInputField.SetActive(!dateInputField.activeInHierarchy);

    }
    public void removeObject() {


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
