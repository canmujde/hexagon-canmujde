using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> hexagons;


    public static HexManager instance;

    public List<GameObject> Hexagons { get => hexagons; set => hexagons = value; }

    private void Awake()
    {
        instance = this;
    }

    public void CheckMatches()
    {
        
        
    }
}
