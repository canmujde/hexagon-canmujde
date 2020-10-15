using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    

    [Header("Params")]
    [SerializeField] private int id;
    [SerializeField] private Color32 color;
    [SerializeField] private Vector2 coords;
    [SerializeField] private Vector2[] dotPositions;




    public Vector2[] DotPositions { get => dotPositions; }
    public int Id { get => id; set => id = value; }
    public Color32 Color { get => color; set => color = value; }
    public Vector2 Coords { get => coords; set => coords = value; }
}
