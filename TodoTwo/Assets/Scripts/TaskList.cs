using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    public GameObject listItemPrefab;
    public Transform heirarchyHook;
    public Image buttonImage;
    public Sprite extended, retracted;
    List<GameObject> taskItems = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GenerateNewItem() {
        GameObject newItem = Instantiate(listItemPrefab, transform.parent);
        newItem.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
        newItem.GetComponent<TaskItem>().ToggleEdit();
        taskItems.Add(newItem);
    }
    public void ToggleList()
    {

        foreach (GameObject item in taskItems) {
            if (item.activeInHierarchy)
                buttonImage.sprite = retracted;
            else
                buttonImage.sprite = extended;
           

            item.SetActive(!item.activeInHierarchy);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
