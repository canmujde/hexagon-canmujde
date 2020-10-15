using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotFixer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Dot"))
        {
            Destroy(collision.gameObject);
        }
    }
}
