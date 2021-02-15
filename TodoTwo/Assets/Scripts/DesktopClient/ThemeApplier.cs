using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeApplier : MonoBehaviour
{
    [Header("Button")]
    public bool button;
    public bool secondaryButton;
    public bool complementaryButton;

    [Header("Background")]
    public bool backgroundSprite;
    public bool background;

    [Header("Logo")]
    public bool logo;
    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    void Refresh()
    {
        if (button)
        {
            if (GetComponent<Button>() != null)
            {
                Image image = GetComponent<Image>();
                Color color = secondaryButton ? (complementaryButton ? ThemeManager.instance.GetCurrentTheme().secondarySuplementaryColor : ThemeManager.instance.GetCurrentTheme().secondaryColor) : (complementaryButton ? ThemeManager.instance.GetCurrentTheme().primarySuplementaryColor : ThemeManager.instance.GetCurrentTheme().primaryColor);
                image.color = color;
            }
        }
        if(backgroundSprite)
        {
            Image image = GetComponent<Image>();
            image.sprite = ThemeManager.instance.GetCurrentTheme().background;
        }
        if (background)
        {
            Image image = GetComponent<Image>();
            image.color = ThemeManager.instance.GetCurrentTheme().mainBackgroundColor;
        }
        if (logo)
        {
            Image image = GetComponent<Image>();
            image.sprite = ThemeManager.instance.GetCurrentTheme().logo;
        }
    }
}
