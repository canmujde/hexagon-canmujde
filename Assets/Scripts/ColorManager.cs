using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{

    [SerializeField] private Color32[] hexColors;

    public static ColorManager instance;

    private void Awake()
    {
        instance = this;
    }

    
    public Color32 RandomColor(int id)
    {
        return hexColors[id];
    }

    public int RandomColorID()
    {
        return Random.Range(0, hexColors.Length);
    }
}
