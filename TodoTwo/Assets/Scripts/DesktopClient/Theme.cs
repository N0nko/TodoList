using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Theme", menuName = "To Do App/Theme")]
public class Theme : ScriptableObject
{
    public Color primaryColor;
    public Color secondaryColor;
    public Color primarySuplementaryColor;
    public Color secondarySuplementaryColor;

    public Color lightTextColor;
    public Color darkTextColor;

    public Color mainBackgroundColor;


    public Sprite logo;
    public Sprite background;

}
