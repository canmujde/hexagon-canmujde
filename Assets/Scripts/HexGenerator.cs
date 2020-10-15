using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGenerator : MonoBehaviour
{

    [Header("Tile Params")]
    [SerializeField] private GameObject OriginalHexagon;
    [SerializeField] private int height;
    [SerializeField] private int width;
    [Header("Realtime")]
    [SerializeField] private int size;
    [Space(25)]

    [SerializeField] private float xGapBetweenHexes;
    [SerializeField] private float yGapBetweenHexes;

    [SerializeField] private Transform hexParent;
    
    float startX = -1.75F;
    float startY = -3.025F;

    public static HexGenerator instance;

    public int Size { get => size; set => size = value; }
    public int Height { get => height;}
    public int Width { get => width; }
    public Transform HexParent { get => hexParent; set => hexParent = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        Size = Height * Width;
        HexManager.instance.Hexagons = new List<GameObject>();
        bool top = true;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                GameObject hex = Instantiate(OriginalHexagon);
                HexManager.instance.Hexagons.Add(hex);
                SetPosition(hex,(startX*2) +  x, (startY*2) + y, top);
                
                SetName(hex,x,y);
                SetParent(hex);
                SetProperties(hex,x,y);
                top = !top;
            }
        }
        Debug.Log("HexGen Over");
        CheckSameGroupOfColor();
        
    }

    private void CheckSameGroupOfColor() //Check same colors between i and i+1 and if they are same color recolor.
    {
        for (int i = 0; i < HexManager.instance.Hexagons.Count; i++)
        {
            if (i == HexManager.instance.Hexagons.Count-1) break;
            if (HexManager.instance.Hexagons[i].GetComponent<Hex>().Color.Equals(HexManager.instance.Hexagons[i + 1].GetComponent<Hex>().Color))
            {
                Color32 oldColor = HexManager.instance.Hexagons[i + 1].GetComponent<Hex>().Color;
                HexManager.instance.Hexagons[i + 1].GetComponent<SpriteRenderer>().color = 
                    HexManager.instance.Hexagons[i + 1].GetComponent<Hex>().Color = 
                        ColorManager.instance.RandomColor(HexManager.instance.Hexagons[i + 1].GetComponent<Hex>().Id = ReColor(oldColor));
            }

        }
        
    }
    private int ReColor(Color32 oldColor)
    {
        int colorID = ColorManager.instance.RandomColorID();
        Color32 newColor = ColorManager.instance.RandomColor(colorID);
        do
        {
            colorID = ColorManager.instance.RandomColorID();
            newColor = ColorManager.instance.RandomColor(colorID);
        } while (newColor.Equals(oldColor));
        
            return colorID;
       
    }

    private void SetProperties(GameObject hex, int x, int y)
    {
        hex.GetComponent<Hex>().Id = ColorManager.instance.RandomColorID();
        hex.GetComponent<SpriteRenderer>().color = hex.GetComponent<Hex>().Color = ColorManager.instance.RandomColor(hex.GetComponent<Hex>().Id);
        hex.GetComponent<Hex>().Coords = new Vector2(x, y);
    }

    private void SetParent(GameObject hex)
    {
        hex.transform.parent = HexParent;
    }

    private void SetName(GameObject hex, int w, int h)
    {
        hex.name = w +" - "+ h;
    }

    private void SetPosition(GameObject hex, float x, float y, bool top)
    {
        hex.transform.localPosition = top ? new Vector2(x * xGapBetweenHexes, (y * yGapBetweenHexes) + 0.3f) : new Vector2(x * xGapBetweenHexes, y * yGapBetweenHexes);
        DotGenerator.instance.Generate(hex);
    }
}
