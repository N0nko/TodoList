using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{

    private static string accessToken;
    public static void SetAccessToken(string value)
    {
        accessToken = value;
    }
    public string GetAccessToken() => accessToken;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<ListModel> taskLists = new List<ListModel>();
    public List<TaskModel> tasks = new List<TaskModel>();
    public IEnumerator GetLists()
    {
        yield return RequestController.GetRequest("list", accessToken);
        var data = RequestController.GetResponseData();
        Debug.Log(data);
        ReceivedListsData lists = JsonUtility.FromJson<ReceivedListsData>(data);
        Debug.Log(lists.data.count);

        List<ListModel> answer = new List<ListModel>();
        for (int i = 0; i < lists.data.count; i++)
        {
            answer.Add(lists.data.rows[i]);
            Debug.Log(lists.data.rows[i]);
        }
        taskLists = answer;
    }

    public IEnumerator GetTasks(string listId)
    {
        tasks = new List<TaskModel>();
        yield return RequestController.GetRequest("task", accessToken);
        var data = RequestController.GetResponseData();
        Debug.Log(data);
        ReceivedTasksData t = JsonUtility.FromJson<ReceivedTasksData>(data);
        Debug.Log(t.data.count);

        List<TaskModel> answer = new List<TaskModel>();
        for (int i = 0; i < t.data.count; i++)
        {
            if (t.data.rows[i].listId.Equals(listId))
            {
                answer.Add(t.data.rows[i]);
                Debug.Log(t.data.rows[i]);
            }
        }
        tasks = answer;
    }
    public IEnumerator GetAllTasks()
    {
        tasks = new List<TaskModel>();
        yield return RequestController.GetRequest("task", accessToken);
        var data = RequestController.GetResponseData();
        Debug.Log(data);
        ReceivedTasksData t = JsonUtility.FromJson<ReceivedTasksData>(data);
        Debug.Log(t.data.count);

        List<TaskModel> answer = new List<TaskModel>();
        for (int i = 0; i < t.data.count; i++)
        {
            answer.Add(t.data.rows[i]);
            Debug.Log(t.data.rows[i]);
        }
        tasks = answer;
    }
}
[System.Serializable]
public class ListModel
{
    public string id;
    public string name;
    public string color;
    public string userId;
    public string updatedAt;
    public string createdAt;
}
[System.Serializable]
public class ListsModel
{
    public int count;
    public ListModel[] rows;
}
[System.Serializable]
public class ReceivedListsData
{
    public ListsModel data;
}

[System.Serializable]
public class TaskModel
{
    public string id;
    public string name;
    public string status;
    public string deadline;
    public string listId;
    public string updatedAt;
    public string createdAt;
}
[System.Serializable]
public class TasksModel
{
    public int count;
    public TaskModel[] rows;
}
[System.Serializable]
public class ReceivedTasksData
{
    public TasksModel data;
}
