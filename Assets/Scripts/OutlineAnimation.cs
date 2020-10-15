using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineAnimation : MonoBehaviour
{
    [SerializeField] private float amp;
    [SerializeField] private float speed;

    void FixedUpdate()
    {
        transform.localScale = Vector3.one + Vector3.one * amp * Mathf.Abs(Mathf.Sin(speed * 0.5f * Time.time));
    }
}
