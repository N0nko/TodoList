using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISystem : MonoBehaviour
{
    public List<TaskListNew> taskLists = new List<TaskListNew>();
    public static UISystem instance;

    public TaskListNew currentList;

    public CanvasGroup listView, taskView;
    public Image taskViewBlue, taskViewProgress;
    public GameObject listPrefab, taskPrefab, taskViewObject;

    public TMP_InputField newListName;
    public Button newListButton;
    public Image newListForm;

    public Image newTaskForm;
    public TMP_InputField newTaskName, newTaskDeadline;

    private float currentListViewAlpha = 1f;
    private float currentTaskCircleSize = 1;
    private float currentTaskViewHolderAlpha = 0;
    private float currentTaskViewAlpha = 1;
    public float currentFillAmount = 0;

    public SessionController sessionController;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public TaskListNew GetList(string listId)
    {
        foreach(TaskListNew list in taskLists)
        {
            if(list.id == listId)
            {
                return list;
            }
        }
        return null;
    }

    public void GenerateTaskLists()
    {
        for(int i = 0; i < sessionController.taskLists.Count; i++)
        {
            GameObject obj = Instantiate(listPrefab, listView.transform);
            obj.GetComponent<RectTransform>().localScale = Vector3.one;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -3000);
            obj.GetComponent<TaskListNew>().targetPosition = new Vector2(0, -700 + (-200 * i));
            obj.GetComponent<TaskListNew>().listName = sessionController.taskLists[i].name;
            obj.GetComponent<TaskListNew>().id = sessionController.taskLists[i].id;
            obj.GetComponent<Button>().onClick.AddListener(delegate { EnterTaskView(obj.GetComponent<TaskListNew>()); });
            taskLists.Add(obj.GetComponent<TaskListNew>());
        }
    }

    public void GenerateTasks(TaskListNew list)
    {
        int count = 0;
        //RemoveAllTaskObjects();
        for (int i = 0; i < sessionController.tasks.Count; i++)
        {
            if (sessionController.tasks[i].listId == list.id)
            {
                GameObject obj = Instantiate(taskPrefab, taskView.transform);
                obj.GetComponent<RectTransform>().localScale = Vector3.one;
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -3000);
                obj.GetComponent<TaskNew>().targetPosition = new Vector2(0, -700 + (-280 * count));
                obj.GetComponent<TaskNew>().taskName = sessionController.tasks[i].name;
                obj.GetComponent<TaskNew>().deadline = sessionController.tasks[i].deadline;
                obj.GetComponent<TaskNew>().taskId = sessionController.tasks[i].id;
                obj.GetComponent<TaskNew>().listId = sessionController.tasks[i].listId;
                obj.GetComponent<TaskNew>().SetCompleted(sessionController.tasks[i].status == "done" ? true : false);
                obj.SetActive(false);
                GetList(sessionController.tasks[i].listId).tasks.Add(obj.GetComponent<TaskNew>());
                count++;
            }
        }
    }

    public void EnterTaskView(TaskListNew list)
    {
        StartCoroutine(TaskViewCoroutine(list));
        currentList = list;
    }
    public void GoBackToListView()
    {
        StartCoroutine(ReturnToListViewCoroutine());
    }
    public void RemoveAllTaskObjects()
    {
        foreach(TaskListNew list in taskLists)
        {
            foreach(TaskNew task in list.tasks)
            {
                Destroy(task.gameObject);
            }
            list.tasks.Clear();
        }
    }

    public void MapTasksToLists()
    {
        foreach(TaskListNew list in taskLists)
        {
            GenerateTasks(list);
        }
    }

    IEnumerator TaskViewCoroutine(TaskListNew list)
    {
        foreach (TaskListNew taskList in taskLists)
        {
            foreach(TaskNew task in taskList.tasks)
            {
                task.gameObject.SetActive(false);
            }
            
        }
        foreach (TaskNew task in list.tasks)
        {
            task.gameObject.SetActive(true);
        }

        taskViewObject.transform.GetChild(0).GetChild(2).gameObject.GetComponent<TMPro.TMP_Text>().text = list.listName;
        taskViewBlue.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        taskViewProgress.fillAmount = 0;
        currentFillAmount = 0;
        currentListViewAlpha = 0;
        currentTaskViewAlpha = 1;
        currentTaskViewHolderAlpha = 0;
        currentTaskCircleSize = 1;
        listView.interactable = false;
        yield return new WaitForSeconds(0.5f);
        
        taskViewObject.gameObject.SetActive(true);
        currentTaskCircleSize = 3000;
        yield return new WaitForSeconds(0.8f);
        currentTaskViewHolderAlpha = 1;
        yield return new WaitForSeconds(0.3f);
        currentFillAmount = list.GetProgress();
    }
    public void CreateNewTask()
    {
        StartCoroutine(TaskCreateEnumerator(currentList.id, newTaskName.text.Trim(), newTaskDeadline.text.Trim()));
    }
    class PostTaskModel
    {
        public string name;
        public string status;
        public string deadline;
        public string listId;
    }
    class PostListModel
    {
        public string name;
        public string color = "#000000";
    }
    public void CreateList()
    {
        StartCoroutine(CreateListEnumerator(newListName.text.Trim()));
    }
    IEnumerator CreateListEnumerator(string name)
    {
        PostListModel t = new PostListModel();
        t.name = name;
        var to = JsonUtility.ToJson(t);
        var bytes = System.Text.Encoding.UTF8.GetBytes(to);
        yield return RequestController.PostRequest("list", bytes, sessionController.GetAccessToken());

        ListModel result = JsonUtility.FromJson<ListModel>(RequestController.GetResponseData());
        GameObject obj = Instantiate(listPrefab, listView.transform);
        obj.GetComponent<RectTransform>().localScale = Vector3.one;
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -3000);
        obj.GetComponent<TaskListNew>().listName = result.name;
        obj.GetComponent<TaskListNew>().id = result.id;
        obj.GetComponent<Button>().onClick.AddListener(delegate { EnterTaskView(obj.GetComponent<TaskListNew>()); });
        taskLists.Add(obj.GetComponent<TaskListNew>());

        newListForm.gameObject.SetActive(false);
    }
    public void RemoveList(string listId)
    {
        StartCoroutine(RemoveListEnumerator(listId));
    }
    IEnumerator RemoveListEnumerator(string listId)
    {
        Debug.Log(listId);
        yield return RequestController.DeleteRequest("list/" + listId, sessionController.GetAccessToken());
    }

    IEnumerator TaskCreateEnumerator(string listId, string taskName, string deadline)
    {
        PostTaskModel model = new PostTaskModel
        {
            listId = listId,
            name = taskName,
            status = "none",
            deadline = deadline
        };
        var m = JsonUtility.ToJson(model);
        Debug.Log(m);
        var modelBytes = System.Text.Encoding.UTF8.GetBytes(m);
        
        yield return RequestController.PostRequest("task", modelBytes, sessionController.GetAccessToken());
        TaskModel result = JsonUtility.FromJson<TaskModel>(RequestController.GetResponseData());
        GameObject obj = Instantiate(taskPrefab, taskView.transform);
        obj.GetComponent<RectTransform>().localScale = Vector3.one;
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -3000);
        obj.GetComponent<TaskNew>().taskName = taskName;
        obj.GetComponent<TaskNew>().deadline = deadline;
        obj.GetComponent<TaskNew>().taskId = result.id;
        obj.GetComponent<TaskNew>().listId = result.listId;
        obj.GetComponent<TaskNew>().SetCompleted(false);
        GetList(listId).tasks.Add(obj.GetComponent<TaskNew>());
        currentFillAmount = GetList(listId).GetProgress();

        newTaskForm.gameObject.SetActive(false);
    }

    public void RemoveTask(string taskId)
    {
        StartCoroutine(TaskRemoveEnumerator(taskId));
    }

    IEnumerator TaskRemoveEnumerator(string taskId)
    {
        yield return RequestController.DeleteRequest("task/" + taskId, sessionController.GetAccessToken());
    }

    IEnumerator ReturnToListViewCoroutine()
    {
        
        currentTaskViewAlpha = 0;
        yield return new WaitForSeconds(0.9f);
        taskViewObject.SetActive(false);
        currentListViewAlpha = 1;
        listView.interactable = true;
        //RemoveAllTaskObjects();
    }

    // Update is called once per frame
    void Update()
    {
        taskViewProgress.fillAmount = Mathf.Lerp(taskViewProgress.fillAmount, currentFillAmount, Time.deltaTime*6);
        listView.alpha = Mathf.Lerp(listView.alpha, currentListViewAlpha, Time.deltaTime * 6);
        taskView.alpha = Mathf.Lerp(taskView.alpha, currentTaskViewHolderAlpha, Time.deltaTime * 6);
        taskViewObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(taskViewObject.GetComponent<CanvasGroup>().alpha, currentTaskViewAlpha, Time.deltaTime * 6);
        taskViewBlue.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(taskViewBlue.GetComponent<RectTransform>().sizeDelta, new Vector2(currentTaskCircleSize, currentTaskCircleSize), Time.deltaTime * 2);
        foreach (TaskListNew taskList in taskLists)
        {
            
            for (int i = 0; i < taskList.tasks.Count; i++)
            {
                taskList.tasks[i].targetPosition = new Vector2(0, -700 + (-280 * i));
            }
        }

        for(int i = 0; i < taskLists.Count; i++)
        {
            taskLists[i].targetPosition = new Vector2(0, -700 + (-200 * i));
        }
    }
}
