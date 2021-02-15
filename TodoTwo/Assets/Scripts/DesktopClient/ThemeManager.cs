using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{

    public static ThemeManager instance;
    private void Awake()
    {
        instance = this;
    }
    public Theme currentTheme;

    public Theme GetCurrentTheme() => currentTheme;
}
