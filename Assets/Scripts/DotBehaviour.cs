using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotBehaviour : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform outline;

    [SerializeField] private List<GameObject> hexagons = new List<GameObject>();
    [SerializeField] private bool rotating;
    [SerializeField] private float targetRotZ;
    [SerializeField] private SpriteRenderer dotRenderer;
    [SerializeField] private SpriteRenderer outlineRenderer;

    public int Id { get => id; set => id = value; }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        SetOutlineRotation();
    }

    private void SetOutlineRotation()
    {
        if (id % 2 == 0) { outline.eulerAngles = Vector3.forward * 60; }
        else { outline.eulerAngles = Vector3.zero; }
    }

    public void Select()
    {
        dotRenderer.enabled = true;
        outlineRenderer.enabled = true;

        for (int i = 0; i < hexagons.Count; i++)
        {
            hexagons[i].transform.parent = transform;
        }
    }

    public void Deselect()
    {
        dotRenderer.enabled = false;
        outlineRenderer.enabled = false;
        for (int i = 0; i < hexagons.Count; i++)
        {
            hexagons[i].transform.parent = HexGenerator.instance.HexParent;
        }
    }

    public IEnumerator Rotate(bool cw)
    {
        if (rotating) yield break;

        rotating = true;
        if (cw)
        {
            
            if (transform.eulerAngles.z>-120)
            {
                float duration = .2f;
                float startRotation = transform.eulerAngles.z;
                float endRotation = startRotation + -120f;
                float t = 0.0f;
                while (t < duration)
                {
                    t += Time.deltaTime;
                    float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
                    yield return null;
                }
            }
            // check is match!
            HexManager.instance.CheckMatches();
            yield return new WaitForSeconds(.2f);
            
            if (transform.eulerAngles.z > -240)
            {
                float duration = .2f;
                float startRotation = transform.eulerAngles.z;
                float endRotation = startRotation + -120f;
                float t = 0.0f;
                while (t < duration)
                {
                    t += Time.deltaTime;
                    float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
                    yield return null;
                }
            }
            // check is match!
            yield return new WaitForSeconds(.2f);
            if (transform.eulerAngles.z > -359.9f)
            {
                float duration = .2f;
                float startRotation = transform.eulerAngles.z;
                float endRotation = startRotation + -120f;
                float t = 0.0f;
                while (t < duration)
                {
                    t += Time.deltaTime;
                    float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
                    yield return null;
                }
            }

        }

        else
        {
            if (transform.eulerAngles.z < 120)
            {
                float duration = .2f;
                float startRotation = transform.eulerAngles.z;
                float endRotation = startRotation + 120f;
                float t = 0.0f;
                while (t < duration)
                {
                    t += Time.deltaTime;
                    float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
                    yield return null;
                }
            }
            // check is match!
            yield return new WaitForSeconds(.2f);

            if (transform.eulerAngles.z < 240)
            {
                float duration = .2f;
                float startRotation = transform.eulerAngles.z;
                float endRotation = startRotation + 120f;
                float t = 0.0f;
                while (t < duration)
                {
                    t += Time.deltaTime;
                    float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
                    yield return null;
                }
            }
            // check is match!
            yield return new WaitForSeconds(.2f);
            if (transform.eulerAngles.z < 359.9f)
            {
                float duration = .2f;
                float startRotation = transform.eulerAngles.z;
                float endRotation = startRotation + 120f;
                float t = 0.0f;
                while (t < duration)
                {
                    t += Time.deltaTime;
                    float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
                    yield return null;
                }
            }
        }

        rotating = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hexagons.Contains(other.gameObject) && other.CompareTag("Hexagon")) 
        { 
            hexagons.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (hexagons.Contains(other.gameObject) && other.CompareTag("Hexagon")) { hexagons.Remove(other.gameObject); }
    }
}
