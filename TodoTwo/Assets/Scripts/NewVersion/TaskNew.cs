using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskNew : MonoBehaviour
{
    // Start is called before the first frame update
    bool completed = false;
    public bool GetCompleted() => completed;
    public void SetCompleted(bool value)
    {
        
        completed = value;
    }
    class PutTaskModel
    {
        public string name;
        public string status;
        public string deadline;
    }
    IEnumerator CompleteToggleEnumerator()
    {
        PutTaskModel model = new PutTaskModel();
        model.name = taskName;
        model.status = GetCompleted() ? "done" : "none";
        model.deadline = deadline;
        var f = JsonUtility.ToJson(model);
        Debug.Log(f);
        Debug.Log(taskId);
        var bytes = System.Text.Encoding.UTF8.GetBytes(f);
        yield return RequestController.PutRequest("task/" + taskId, bytes, UISystem.instance.sessionController.GetAccessToken());
        UISystem.instance.currentFillAmount = UISystem.instance.currentList.GetProgress();
    }
    public string taskName;
    public string listId;
    public string taskId;
    public string deadline;
    public Vector2 targetPosition;
    public void ToggleComplete()
    {
        SetCompleted(!GetCompleted());
        StartCoroutine(CompleteToggleEnumerator());
    }

    public void Remove()
    {
        UISystem.instance.RemoveTask(taskId);
        Debug.Log(listId);
        UISystem.instance.GetList(listId).tasks.Remove(this);
        UISystem.instance.currentFillAmount = UISystem.instance.currentList.GetProgress();
        Destroy(this.gameObject);
    }
    void Start()
    {
        //transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener( delegate { ToggleComplete(); });
        //transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(delegate { Remove(); });
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>().text = taskName;
        //transform.GetChild(3).gameObject.GetComponent<TMPro.TMP_Text>().text = "Until " + deadline;
        //transform.GetChild(2).gameObject.GetComponent<Image>().color = GetCompleted() ? Color.green : Color.white;
        //GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, targetPosition, Time.deltaTime * 6);
    }
}
