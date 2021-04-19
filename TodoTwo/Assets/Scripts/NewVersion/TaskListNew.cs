using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskListNew : MonoBehaviour
{
    public string id;
    public List<TaskNew> tasks = new List<TaskNew>();
    public Vector2 targetPosition;
    public string listName;
    // Start is called before the first frame update
    void Start()
    {
        //transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(delegate { Remove(); });
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = listName;
        //transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = GetProgress();
        //GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, targetPosition, Time.deltaTime * 6);
    }

    public void Remove()
    {
        UISystem.instance.RemoveList(id);
        UISystem.instance.taskLists.Remove(this);
        Destroy(this.gameObject);
    }
    public float GetProgress()
    {
        int count = 0;
        foreach(TaskNew t in tasks)
        {
            count += t.GetCompleted() ? 1 : 0;
        }
        return tasks.Count == 0 ? 0 : ((float)count / (float)tasks.Count);
    }
}
