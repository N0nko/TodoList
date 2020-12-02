using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskListController : MonoBehaviour
{
    public GameObject listPrefab;
    public GameObject newItemPrefab;
    public Transform heirarchyHook;
    List<GameObject> lists = new List<GameObject>();
    // Start is called before the first frame update

    public void GenerateNewList()
    {
        GameObject newItem = Instantiate(listPrefab, transform);
        newItem.transform.SetSiblingIndex(heirarchyHook.GetSiblingIndex() - 1);
        GameObject addNewItem = Instantiate(newItemPrefab, transform);
        addNewItem.transform.SetSiblingIndex(heirarchyHook.GetSiblingIndex() - 1);
        addNewItem.GetComponent<Button>().onClick.AddListener(delegate { newItem.GetComponent<TaskList>().GenerateNewItem(); });
        newItem.GetComponent<TaskList>().heirarchyHook = addNewItem.transform;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
