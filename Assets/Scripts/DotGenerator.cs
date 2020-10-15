using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotGenerator : MonoBehaviour
{
    [SerializeField] private GameObject OriginalDot;
    [SerializeField] private Transform dotParent;

    private int dotID;

    public static DotGenerator instance;

    
    private void Awake()
    {
        instance = this;
        dotID = 0;
    }

    public void Generate(GameObject hex)
    {
        GameObject dot = Instantiate(OriginalDot, (Vector2)hex.transform.position + hex.GetComponent<Hex>().DotPositions[0], Quaternion.identity);
        GameObject dot2 = Instantiate(OriginalDot, (Vector2)hex.transform.position + hex.GetComponent<Hex>().DotPositions[1], Quaternion.identity);

        dot.GetComponent<DotBehaviour>().Id = dotID;
        dot2.GetComponent<DotBehaviour>().Id = dotID + 1;
        dotID += 2;

        dot.transform.parent = dotParent;
        dot2.transform.parent = dotParent;
    }

}
