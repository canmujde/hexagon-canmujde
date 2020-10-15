using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Input : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private RaycastHit2D[] touchHits;
    
    GameObject lastSelectedDot = null;
    [Header("Params")]
    [SerializeField] private float swipeDistance;
    [SerializeField] private float circleCastRadius = 1.0f;
    void Update()
    {
        HandleInput();
    }

    private void Start()
    {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Hexagon");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
    private void HandleInput()
    {
        if (GameManager.instance.State != GameState.Playing) return;
#if UNITY_EDITOR || UNITY_STANDALONE
        // Standalone Input
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float d = Vector2.Distance(startPos, endPos);

            if (d < swipeDistance)
            {
                OnTouch(endPos);
            }
            else
            {
                OnSwipe();
            }
        }
#endif
#if UNITY_ANDROID || UNITY_IOS
        // Mobile Input
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase) {

                case TouchPhase.Began:
                    startPos = Camera.main.ScreenToWorldPoint(touch.position);
                    break;
                case TouchPhase.Ended:

                    endPos = Camera.main.ScreenToWorldPoint(touch.position);

                    float d = Vector2.Distance(startPos, endPos);

                    if(d < swipeDistance) {
                        OnTouch(endPos);
                    } else {
                        OnSwipe();
                    }
                    break;
            }
        }
#endif

    }

    private void OnSwipe()
    {
        Debug.Log("OnSwipe");
        if (!lastSelectedDot)
            return;

        Vector2 origin = lastSelectedDot.transform.position;
        Vector2 start = startPos - origin;
        Vector2 end = endPos - origin;

        Swipe(start, end);
        
    }

    private void OnTouch(Vector3 endPos)
    {
        Vector3 touchWorldPosition = endPos;
        touchHits = Physics2D.CircleCastAll(touchWorldPosition, circleCastRadius, Vector2.zero);
        float minDist = 10.0f;
        GameObject selectedDot = null;
        foreach (RaycastHit2D hit in touchHits)
        {
            
            Vector2 hitPos = hit.collider.gameObject.transform.position;
            
            float distance = SqrDistance(touchWorldPosition, hitPos);
            if (distance < minDist)
            {
                minDist = distance;
                selectedDot = hit.collider.gameObject;
            }
        }

        Touch(selectedDot);
        Debug.Log("OnTouch");

    }

    private void Touch(GameObject selectedDot)
    {
        if (!selectedDot.CompareTag("Dot") || !selectedDot) return;

        if (!lastSelectedDot) lastSelectedDot = selectedDot;

        if (selectedDot != lastSelectedDot)
        {
            Debug.Log("Deselect");
            lastSelectedDot.GetComponent<DotBehaviour>().Deselect();
        }

        Debug.Log("Select");
        lastSelectedDot = selectedDot;
        lastSelectedDot.GetComponent<DotBehaviour>().Select();
    }

    private void Swipe(Vector2 start, Vector2 end)
    {
        if (Vector2.SignedAngle(start, end) < 0.0f) //CW
        {
            StartCoroutine(lastSelectedDot.GetComponent<DotBehaviour>().Rotate(true));
            Debug.Log("CW");
        }
        else //C_CW
        {
            StartCoroutine(lastSelectedDot.GetComponent<DotBehaviour>().Rotate(false));
            Debug.Log("C-CW");
        }
    }

    static float SqrDistance(Vector2 a, Vector2 b)
    {
        return Mathf.Pow(b.x - a.x, 2) + Mathf.Pow(b.y - a.y, 2);
    }
}
