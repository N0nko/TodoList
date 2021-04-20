using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Histograms : MonoBehaviour
{
    public static Histograms instance;
    public SessionController session;
    public Transform histogramRoot;
    public GameObject histogramContainer;
    public GameObject histogramSlider;
 
    private void Awake()
    {
        instance = this;
    }


    public void GenerateHistograms()
    {
        int totalSliderCount = 0;
        foreach (ListModel list in session.taskLists)
        {
            string listID = list.id;
            GameObject container = Instantiate(histogramContainer, histogramRoot);
            int sliderCount = 0;
            foreach (TaskModel task in session.tasks)
            {
                if (listID != task.listId)
                    continue;
                GameObject slider = Instantiate(histogramSlider, container.transform);
                slider.GetComponentInChildren<TextMeshProUGUI>().text = task.name;
              //  slider.GetComponentInChildren<Slider>().maxValue = task.hours;
                sliderCount++;
                totalSliderCount++;
            }
            RectTransform rt = container.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(sliderCount * 100 + sliderCount, rt.sizeDelta.y);
        }
    }

    private static int GetListID(ListModel list)
    {
        int listID;
        if (int.TryParse(list.id, out listID) == false)
        {
            Debug.Log("TryParse failed.");
        }
        return listID;
    }
}
